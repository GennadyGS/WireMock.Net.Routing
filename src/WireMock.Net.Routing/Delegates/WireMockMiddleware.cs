namespace WireMock.Net.Routing.Delegates;

public delegate WireMockHttpRequestHandler WireMockMiddleware(WireMockHttpRequestHandler next);
