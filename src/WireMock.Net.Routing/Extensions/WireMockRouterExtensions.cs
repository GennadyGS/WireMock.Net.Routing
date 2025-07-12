using Newtonsoft.Json;
using WireMock.Net.Routing.Models;

namespace WireMock.Net.Routing.Extensions;

public static class WireMockRouterExtensions
{
    public static WireMockRouter MapGet(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, object?> requestHandler) =>
        source.Map(HttpMethod.Get.Method, pattern, requestHandler);

    public static WireMockRouter MapGet(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, Task<object?>> requestHandler) =>
        source.Map(HttpMethod.Get.Method, pattern, requestHandler);

    public static WireMockRouter MapPost(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, object?> requestHandler) =>
        source.Map(HttpMethod.Post.Method, pattern, requestHandler);

    public static WireMockRouter MapPost(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, Task<object?>> requestHandler) =>
        source.Map(HttpMethod.Post.Method, pattern, requestHandler);

    public static WireMockRouter MapPost<TRequest>(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo<TRequest>, object?> requestHandler,
        JsonSerializerSettings? jsonSettings = null) =>
        source.Map(HttpMethod.Post.Method, pattern, requestHandler, jsonSettings);

    public static WireMockRouter MapPost<TRequest>(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo<TRequest>, Task<object?>> requestHandler,
        JsonSerializerSettings? jsonSettings = null) =>
        source.Map(HttpMethod.Post.Method, pattern, requestHandler, jsonSettings);

    public static WireMockRouter MapPut(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, object?> requestHandler) =>
        source.Map(HttpMethod.Put.Method, pattern, requestHandler);

    public static WireMockRouter MapPut(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, Task<object?>> requestHandler) =>
        source.Map(HttpMethod.Put.Method, pattern, requestHandler);

    public static WireMockRouter MapPut<TRequest>(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo<TRequest>, object?> requestHandler,
        JsonSerializerSettings? jsonSettings = null) =>
        source.Map(HttpMethod.Put.Method, pattern, requestHandler, jsonSettings);

    public static WireMockRouter MapPut<TRequest>(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo<TRequest>, Task<object?>> requestHandler,
        JsonSerializerSettings? jsonSettings = null) =>
        source.Map(HttpMethod.Put.Method, pattern, requestHandler, jsonSettings);

    public static WireMockRouter MapDelete(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, object?> requestHandler) =>
        source.Map(HttpMethod.Delete.Method, pattern, requestHandler);

    public static WireMockRouter MapDelete(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo, Task<object?>> requestHandler) =>
        source.Map(HttpMethod.Delete.Method, pattern, requestHandler);

    public static WireMockRouter MapDelete<TRequest>(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo<TRequest>, object?> requestHandler,
        JsonSerializerSettings? jsonSettings = null) =>
        source.Map(HttpMethod.Delete.Method, pattern, requestHandler, jsonSettings);

    public static WireMockRouter MapDelete<TRequest>(
        this WireMockRouter source,
        string pattern,
        Func<WireMockRequestInfo<TRequest>, Task<object?>> requestHandler,
        JsonSerializerSettings? jsonSettings = null) =>
        source.Map(HttpMethod.Delete.Method, pattern, requestHandler, jsonSettings);
}
