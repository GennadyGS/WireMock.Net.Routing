# WireMock.Net.Routing

![NuGet](https://img.shields.io/nuget/v/WireMock.Net.Routing?style=flat-square)
![Build](https://img.shields.io/github/actions/workflow/status/GennadyGS/WireMock.Net.Routing/build.yml?branch=master&style=flat-square)
[![.NET](https://img.shields.io/badge/.NET-8.0%2B-blue.svg)](https://dotnet.microsoft.com/)
![License](https://img.shields.io/github/license/GennadyGS/WireMock.Net.Routing?style=flat-square)

**WireMock.Net.Routing** extends [WireMock.Net](https://github.com/wiremock/wiremock) with modern, minimal-API-style routing for .NET. It provides extension methods for expressive, maintainable, and testable HTTP routing, inspired by [ASP.NET Core Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-9.0).

---

## Features

- Minimal API-style route definitions for WireMock.Net
- Strongly-typed request/response handling
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
using WireMock.Server;
using WireMock.Net.Routing;

var server = WireMockServer.Start();

server.MapGet("/hello", ctx => ctx.Response("Hello, world!"));

// ...
```

---

## Usage

### Minimal Routing Example

```csharp
server.MapPost("/api/items", async ctx => {
    var item = await ctx.BindJsonAsync<Item>();
    // process item
    return ctx.Json(new { success = true });
});
```

### Supported Methods

- `MapGet`, `MapPost`, `MapPut`, `MapDelete`, `MapPatch`, etc.

### Strongly-Typed Request Info

```csharp
server.MapGet("/user/{id}", (WireMockRequestInfo req) => {
    var userId = req.RouteValues["id"];
    // ...
});
```

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
