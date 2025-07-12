namespace WireMock.Net.Routing.Extensions;

internal static class StringExtensions
{
    public static string ToMatchFullStringRegex(this string regex) =>
        $"^{regex}$";
}
