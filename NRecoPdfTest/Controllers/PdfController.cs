using Microsoft.AspNetCore.Mvc;
using NRecoPdfTest.Pdf;

namespace NRecoPdfTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PdfController(IPdfRenderer renderer) : ControllerBase
{
    public record RenderRequest(string Html, bool Landscape = false);

    /// <summary>Renders the posted HTML as a PDF.</summary>
    [HttpPost]
    public IActionResult Render(RenderRequest request) =>
        File(renderer.RenderHtml(request.Html, request.Landscape), "application/pdf", "document.pdf");
}
