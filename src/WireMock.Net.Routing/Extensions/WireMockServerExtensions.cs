using WireMock.Matchers;
using WireMock.Net.Routing.Delegates;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WireMock.Net.Routing.Extensions;

public static class WireMockServerExtensions
{
    public static WireMockServer Map(
        this WireMockServer source,
        string method,
        IStringMatcher pathMatcher,
        WireMockHttpRequestHandler httpRequestHandler)
    {
        source
            .Given(Request.Create().WithPath(pathMatcher).UsingMethod(method))
            .RespondWith(Response.Create().WithCallback(req => httpRequestHandler(req)));
        return source;
    }
}
