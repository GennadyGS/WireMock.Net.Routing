using WireMock.Net.Routing.Delegates;

namespace WireMock.Net.Routing.Extensions;

internal static class WireMockHttpRequestHandlerExtensions
{
    public static WireMockHttpRequestHandler UseMiddleware(
        this WireMockHttpRequestHandler handler, WireMockMiddleware middleware) =>
        middleware(handler);

    public static WireMockHttpRequestHandler UseMiddlewareCollection(
        this WireMockHttpRequestHandler handler,
        IReadOnlyCollection<WireMockMiddleware> middlewareCollection) =>
        middlewareCollection.Aggregate(handler, UseMiddleware);
}
