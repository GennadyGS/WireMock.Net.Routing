using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WireMock.Matchers;
using WireMock.Net.Routing.Delegates;
using WireMock.Net.Routing.Extensions;
using WireMock.Net.Routing.Models;
using WireMock.Net.Routing.Utils;
using WireMock.Server;

namespace WireMock.Net.Routing;

public sealed class WireMockRouter
{
    private readonly WireMockServer _server;

    public WireMockRouter(WireMockServer server)
    {
        _server = server;
    }

    public IReadOnlyCollection<WireMockMiddleware> MiddlewareCollection { get; init; } = [];

    public JsonSerializerSettings? DefaultJsonSettings { get; init; }

    public WireMockRouter Map(
        string method, string pattern, Func<WireMockRequestInfo, object?> requestHandler)
    {
        object? CreateResponse(IRequestMessage request) =>
            requestHandler(CreateRequestInfo(request, pattern));

        return Map(method, pattern, CreateResponse);
    }

    public WireMockRouter Map(
        string method, string pattern, Func<WireMockRequestInfo, Task<object?>> requestHandler)
    {
        Task<object?> CreateResponseAsync(IRequestMessage request) =>
            requestHandler(CreateRequestInfo(request, pattern));

        return Map(method, pattern, CreateResponseAsync);
    }

    public WireMockRouter Map<TRequest>(
        string method,
        string pattern,
        Func<WireMockRequestInfo<TRequest>, object?> requestHandler,
        JsonSerializerSettings? jsonSettings = null)
    {
        object? CreateBody(IRequestMessage request) =>
            requestHandler(CreateRequestInfo<TRequest>(request, pattern, jsonSettings));

        return Map(method, pattern, CreateBody);
    }

    private static WireMockRequestInfo CreateRequestInfo(IRequestMessage request, string pattern) =>
        new(request)
        {
            RouteArgs = RoutePattern.GetArgs(pattern, request.Path),
        };

    private static WireMockHttpRequestHandler CreateHttpRequestHandler(
        Func<IRequestMessage, object?> requestHandler) =>
        request => CreateResponseMessageAsync(requestHandler(request));

    private static async Task<ResponseMessage> CreateResponseMessageAsync(object? response)
    {
        var awaitedResponse = response is Task task
            ? await task.ToGenericTaskAsync()
            : response;
        var result = awaitedResponse as IResult ?? Results.Ok(awaitedResponse);
        var httpContext = CreateHttpContext();
        await result.ExecuteAsync(httpContext);
        return await httpContext.Response.ToResponseMessageAsync();
    }

    private static HttpContext CreateHttpContext() =>
        new DefaultHttpContext
        {
            RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
            Response = { Body = new MemoryStream() },
        };

    private WireMockRequestInfo<TRequest> CreateRequestInfo<TRequest>(
        IRequestMessage request, string pattern, JsonSerializerSettings? jsonSettings = null)
    {
        var requestInfo = CreateRequestInfo(request, pattern);
        return new WireMockRequestInfo<TRequest>(requestInfo.Request)
        {
            RouteArgs = requestInfo.RouteArgs,
            Body = requestInfo.Request.GetBodyAsJson<TRequest>(jsonSettings ?? DefaultJsonSettings),
        };
    }

    private WireMockRouter Map(
        string method, string pattern, Func<IRequestMessage, object?> requestHandler)
    {
        var matcher = new RegexMatcher(RoutePattern.ToRegex(pattern), ignoreCase: true);
        var httpRequestHandler =
            CreateHttpRequestHandler(requestHandler).UseMiddlewareCollection(MiddlewareCollection);
        _server.Map(method, matcher, httpRequestHandler);
        return this;
    }
}
