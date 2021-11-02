using System;
using System.IO;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using NDesk.Options;

namespace PDF_PageNo
{
    class Program
    {
        public class StampPageXofY
        {
            static String _src;
            static String _dest;
            static bool _help;
            static void Main(string[] args)

                {
                var options = new OptionSet()
                {
                    { "s|SRC", "Enter Source FIle", arg => _src = arg },
                    { "d|DEST", "Enter Destination FIle", arg => _dest = arg },
                    { "h|Help", "Display Help Section", arg => _help = arg != null}
                };

               var extras = options.Parse(args);

                if ((!_help && !args.Any()) || !extras.Any()) {
                    _help = true;
                }

                if (string.IsNullOrEmpty(_src)) { _src = "C:\\PDFCreate\\Store\\Combined.PDF"; } else { _src = extras[0]; }
                if (string.IsNullOrEmpty(_dest)) { _dest = "C:\\PDFCreate\\Finished.PDF"; } else { _dest = extras[1]; }

                if (_help) {
                    options.WriteOptionDescriptions(Console.Out);
                    return;
                }
                
            FileInfo file = new FileInfo(_dest);
            file.Directory.Create();
            new StampPageXofY().ManipulatePdf(_dest);
        }
            protected void ManipulatePdf(String _dest)
                {
                    PdfReader reader = new PdfReader(_src);
                    PdfWriter writer = new PdfWriter(_dest);
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
