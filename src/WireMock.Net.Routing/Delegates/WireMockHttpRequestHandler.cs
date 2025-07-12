namespace WireMock.Net.Routing.Delegates;

public delegate Task<ResponseMessage> WireMockHttpRequestHandler(IRequestMessage requests);
