namespace WireMock.Net.Routing.Models;

public class WireMockRequestInfo
{
    public WireMockRequestInfo(IRequestMessage request)
    {
        Request = request;
    }

    public IRequestMessage Request { get; }

    public IDictionary<string, object> RouteArgs { get; init; } =
        new Dictionary<string, object>();
}
