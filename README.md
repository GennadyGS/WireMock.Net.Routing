# WireMock.Net.Routing

![NuGet](https://img.shields.io/nuget/v/GennadyGS.WireMock.Net.Routing?style=flat-square)
![Build](https://img.shields.io/github/actions/workflow/status/GennadyGS/WireMock.Net.Routing/build.yml?branch=master&style=flat-square)
[![.NET](https://img.shields.io/badge/.NET-8.0%2B-blue.svg)](https://dotnet.microsoft.com/)
![License](https://img.shields.io/github/license/GennadyGS/WireMock.Net.Routing?style=flat-square)

**WireMock.Net.Routing** extends [WireMock.Net](https://github.com/wiremock/wiremock) with modern, minimal-API-style routing for .NET. It provides extension methods for expressive, maintainable, and testable HTTP routing, inspired by [ASP.NET Core Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-9.0).

---

## Motivation

While [WireMock.Net](https://github.com/WireMock-Net/WireMock.Net) is a powerful tool for HTTP mocking in .NET, its native API for defining routes and request handlers can be verbose and require significant boilerplate. Setting up even simple endpoints often involves multiple chained method calls, manual parsing of request data, and repetitive configuration, which can make tests harder to read and maintain.

**WireMock.Net.Routing** addresses these pain points by introducing a concise, fluent, and minimal-API-inspired approach to routing. This makes your test code:

- **More readable:** Route definitions are clear and expressive, closely resembling production minimal APIs.
- **Easier to maintain:** Less boilerplate means fewer places for errors and easier refactoring.
- **Faster to write:** Define routes and handlers in a single line, with strong typing and async support.

### Example: Native WireMock.Net vs. WireMock.Net.Routing

#### Native WireMock.Net

```csharp
server.Given(
    Request.Create().WithPath("/hello").UsingGet()
)
.RespondWith(
    Response.Create().WithBody("Hello, world!")
);

server.Given(
    Request.Create().WithPath("/user/*").UsingGet()
)
.RespondWith(
    Response.Create().WithCallback(request =>
    {
        var id = request.PathSegments[1];
        // ...fetch user by id...
        return new ResponseMessage { Body = $"User: {id}" };
    })
);
```

#### With WireMock.Net.Routing

```csharp
router.MapGet("/hello", _ => "Hello, world!");

router.MapGet("/user/{id:int}", requestInfo =>
{
    var id = requestInfo.RouteArgs["id"];
    // ...fetch user by id...
    return $"User: {id}";
});
```

With **WireMock.Net.Routing**, you get:

- Minimal, one-line route definitions
- Typed route parameters (e.g., `{id:int}`)
- Direct access to parsed route arguments and request bodies
- Async handler support

This leads to more maintainable, scalable, and production-like test code.

---

## Features

- Minimal API-style route definitions for WireMock.Net
- Strongly-typed request handling
- Routing parameters with constraints (`int` and `string` are currently supported)
- Asynchronous handlers
- Fluent, composable routing extensions
- Easy integration with existing WireMock.Net servers
- .NET 8+ support

---

## Installation

Install from NuGet:

```shell
dotnet add package WireMock.Net.Routing
```

---

## Quick Start

```csharp
using System.Net.Http.Json;
using WireMock.Net.Routing;
using WireMock.Net.Routing.Extensions;
using WireMock.Server;

var server = WireMockServer.Start();
var router = new WireMockRouter(server);

router.MapGet("/hello", _ => "Hello, world!");

using var client = server.CreateClient();
var result = await client.GetFromJsonAsync<string>("/hello");
// Hello, world!
```

---

## Usage

### Routing with route parameters

```csharp
router.MapGet("/user/{id:int}", async requestInfo => {
    var userId = requestInfo.RouteArgs["id"];
    // var user = await ...
    return user;
});
```

### Strongly-Typed Request Info

```csharp
router.MapPost<Item>("/api/items", requestInfo => {
    var item = requestInfo.Body!;
    // process item
    return Results.Json(new { success = true });
});
```

### Supported Methods

- `MapGet`, `MapPost`, `MapPut`, `MapDelete`
---

## Documentation

- [API Reference](./src/WireMock.Net.Routing/)
- [WireMock.Net Documentation](https://github.com/WireMock-Net/WireMock.Net)

---

## Contributing

Contributions are welcome! Please open issues or pull requests. See [CONTRIBUTING.md](CONTRIBUTING.md) if available.

---

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.

---

## Support & Feedback

For questions, suggestions, or issues, please use the [GitHub Issues](https://github.com/GennadyGS/WireMock.Net.Routing/issues) page.
