namespace NRecoPdfTest.Pdf;

public interface IPdfRenderer
{
    byte[] RenderHtml(string html, bool landscape = false);
}
