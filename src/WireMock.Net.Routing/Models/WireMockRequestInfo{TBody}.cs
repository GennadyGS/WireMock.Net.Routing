namespace WireMock.Net.Routing.Models;

public sealed class WireMockRequestInfo<TBody> : WireMockRequestInfo
{
    public WireMockRequestInfo(IRequestMessage request)
        : base(request)
    {
    }

    public TBody? Body { get; init; }
}
