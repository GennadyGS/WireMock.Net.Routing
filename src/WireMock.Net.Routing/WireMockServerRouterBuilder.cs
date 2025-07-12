using System.Collections.Concurrent;
using Newtonsoft.Json;
using WireMock.Net.Routing.Delegates;
using WireMock.Server;

namespace WireMock.Net.Routing;

public sealed class WireMockServerRouterBuilder
{
    private readonly WireMockServer _server;

    private readonly ConcurrentQueue<WireMockMiddleware> _middlewareCollection = new();

    private JsonSerializerSettings? _defaultJsonSettings;

    public WireMockServerRouterBuilder(WireMockServer server)
    {
        _server = server;
    }

    public WireMockRouter Build() =>
        new(_server)
        {
            MiddlewareCollection = _middlewareCollection,
            DefaultJsonSettings = _defaultJsonSettings,
        };

    public WireMockServerRouterBuilder Use(WireMockMiddleware middleware)
    {
        _middlewareCollection.Enqueue(middleware);
        return this;
    }

    public WireMockServerRouterBuilder WithDefaultJsonSettings(
        JsonSerializerSettings? defaultJsonSettings)
    {
        _defaultJsonSettings = defaultJsonSettings;
        return this;
    }
}
