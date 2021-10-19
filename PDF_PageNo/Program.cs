using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PDF_PageNo
{
    class Program 
    {
        public class StampPageXofY
        {
            public static String DEST = "C:/PDFCreate/Store/Finished.pdf";
            public static String SRC = "C:/PDFCreate/Store/Combined.pdf";
            static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            file.Directory.Create();
            new StampPageXofY().ManipulatePdf(DEST);
        }
            protected void ManipulatePdf(String dest)
            {
                PdfReader reader = new PdfReader(SRC);
                PdfWriter writer = new PdfWriter(DEST);
                PdfDocument pdfDoc = new PdfDocument(reader, writer);
                Document doc = new Document(pdfDoc);

                int numberOfPages = pdfDoc.GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    doc.ShowTextAligned(new Paragraph("Page " + i + " of " + numberOfPages),
                            20, 20, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
                }
                doc.Close();
            }
        }
    }
}
