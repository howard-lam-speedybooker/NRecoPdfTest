using NReco.PdfGenerator;

namespace NRecoPdfTest.Pdf;

/// <summary>HTML to PDF through NReco (wkhtmltopdf), as SB.Framework's PDFHelper does.</summary>
public class NRecoPdfRenderer : IPdfRenderer
{
    public byte[] RenderHtml(string html, bool landscape = false)
    {
        var converter = new HtmlToPdfConverter
        {
            Orientation = landscape ? PageOrientation.Landscape : PageOrientation.Portrait,
        };
        return converter.GeneratePdf(html);
    }
}
