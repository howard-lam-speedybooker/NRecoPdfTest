using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace NRecoPdfTest.Tests;

public class PdfControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task PostReturnsAPdf()
    {
        var client = factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/pdf",
            new { html = "<h1>From the API</h1>" }, TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/pdf", response.Content.Headers.ContentType?.MediaType);
        var pdf = await response.Content.ReadAsByteArrayAsync(TestContext.Current.CancellationToken);
        PdfAssert.Save(pdf, "api.pdf");
        PdfAssert.IsPdf(pdf);
    }
}
