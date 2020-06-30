using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Decryptorus
{
    public class ToolsNotifResult
    {
        public void createPdf()
        {
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(new FileStream("/myfiles/hello.pdf", FileMode.Create, FileAccess.Write)));
            Document document = new Document(pdfDocument);

            String line = "Hello! Welcome to iTextPdf";
            document.Add(new Paragraph(line));
            document.Close();
            Console.WriteLine("Awesome PDF just got created.");
        }
    }
}