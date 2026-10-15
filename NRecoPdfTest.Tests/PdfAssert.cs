using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NRecoPdfTest.Tests;

static class PdfAssert
{
    public static void IsPdf(byte[] pdf)
    {
        Assert.True(pdf.Length > 1000, $"PDF is only {pdf.Length} bytes");
        Assert.Equal("%PDF-", Encoding.ASCII.GetString(pdf, 0, 5));
        Assert.Contains("%%EOF", Encoding.ASCII.GetString(pdf, pdf.Length - 32, 32));
    }

    public static int PageCount(byte[] pdf) =>
        Regex.Matches(Encoding.Latin1.GetString(pdf), @"/Type\s*/Page[^s]").Count;

    /// <summary>Width and height of the first page's MediaBox, in points.</summary>
    public static (double Width, double Height) FirstPageSize(byte[] pdf)
    {
        var box = Regex.Match(Encoding.Latin1.GetString(pdf),
            @"/MediaBox\s*\[\s*([\d.]+)\s+([\d.]+)\s+([\d.]+)\s+([\d.]+)\s*\]");
        Assert.True(box.Success, "No MediaBox in the PDF");
        double At(int i) => double.Parse(box.Groups[i].Value, CultureInfo.InvariantCulture);
        return (At(3) - At(1), At(4) - At(2));
    }

    /// <summary>Keeps the PDF under bin/.../pdf-output so a run's output can be opened (CI uploads it).</summary>
    public static void Save(byte[] pdf, string name)
    {
        var dir = Path.Combine(AppContext.BaseDirectory, "pdf-output");
        Directory.CreateDirectory(dir);
        File.WriteAllBytes(Path.Combine(dir, name), pdf);
    }
}
