using NRecoPdfTest.Pdf;

namespace NRecoPdfTest.Tests;

public class NRecoPdfRendererTests
{
    readonly NRecoPdfRenderer renderer = new();

    [Fact]
    public void RendersHtmlToPdf()
    {
        var pdf = renderer.RenderHtml("<html><body><h1>Invoice 1001</h1><p>Total &pound;1,500.00</p></body></html>");

        PdfAssert.Save(pdf, "portrait.pdf");
        PdfAssert.IsPdf(pdf);
        Assert.Equal(1, PdfAssert.PageCount(pdf));
    }

    [Fact]
    public void PortraitIsTallerThanWide()
    {
        var (width, height) = PdfAssert.FirstPageSize(renderer.RenderHtml("<p>Portrait</p>"));

        Assert.True(height > width, $"{width} x {height}");
    }

    [Fact]
    public void LandscapeIsWiderThanTall()
    {
        var pdf = renderer.RenderHtml("<p>Landscape</p>", landscape: true);
        var (width, height) = PdfAssert.FirstPageSize(pdf);

        PdfAssert.Save(pdf, "landscape.pdf");
        Assert.True(width > height, $"{width} x {height}");
    }

    [Fact]
    public void PageBreakStartsASecondPage()
    {
        var pdf = renderer.RenderHtml("<p>One</p><div style=\"page-break-after: always\"></div><p>Two</p>");

        PdfAssert.Save(pdf, "two-pages.pdf");
        Assert.Equal(2, PdfAssert.PageCount(pdf));
    }
}
