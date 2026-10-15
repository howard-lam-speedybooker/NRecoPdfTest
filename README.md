# NRecoPdfTest

A test ASP.NET Core (net10.0) API set up like the Frisbeee backend, rendering PDFs with
NReco.PdfGenerator 1.2.1 (the version SB.Framework uses).

- `POST /api/pdf` with `{ "html": "...", "landscape": false }` returns a PDF.
- `dotnet test` renders sample documents and checks the PDF header, page count and orientation.
  The PDFs land in `NRecoPdfTest.Tests/bin/Debug/net10.0/pdf-output/`, and CI uploads them as the
  `rendered-pdfs` artefact.

The free NReco build embeds the Windows wkhtmltopdf binary, so it runs on Windows only (CI uses
`windows-latest`).
