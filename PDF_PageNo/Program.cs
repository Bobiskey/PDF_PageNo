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
            static String _left;
            static String _bottom;
            static bool _help;
            static void Main(string[] args)

                {
                var options = new OptionSet()
                {
                    { "s|SRC", "Enter Source FIle", arg => _src = arg },
                    { "d|DEST", "Enter Destination FIle", arg => _dest = arg },
                    { "l|Left", "Pixels in from the Left side", arg => _left = arg},
                    { "b|Bottom", "Pixels up from the Bottom of the page", arg => _bottom = arg},
                    { "h|Help", "Display Help Section", arg => _help = arg != null}
                };

               var extras = options.Parse(args);

                if ((!_help && !args.Any()) || !extras.Any()) {
                    _help = true;
                }

                if (string.IsNullOrEmpty(_src)) { _src = "C:\\PDFCreate\\Store\\Combined.PDF"; } else { _src = extras[0]; }
                if (string.IsNullOrEmpty(_dest)) { _dest = "C:\\PDFCreate\\Finished.PDF"; } else { _dest = extras[1]; }
                if (string.IsNullOrEmpty(_left)) { _left = "20"; } else { _left = extras[2]; }
                if (string.IsNullOrEmpty(_bottom)) { _bottom = "20"; } else { _bottom = extras[3]; }

                if (_help) {
                    options.WriteOptionDescriptions(Console.Out);
                    return;
                }
                
            FileInfo file = new FileInfo(_dest);
            file.Directory.Create();
            new StampPageXofY().ManipulatePdf(_dest);
        }
            protected void ManipulatePdf(String dest)
                {
                    PdfReader reader = new PdfReader(_src);
                    PdfWriter writer = new PdfWriter(_dest);
                    PdfDocument pdfDoc = new PdfDocument(reader, writer);
                    Document doc = new Document(pdfDoc);

                    int numberOfPages = pdfDoc.GetNumberOfPages();
                    for (int i = 1; i <= numberOfPages; i++)
                    {
                    float lf1 = float.Parse(_left);
                    float bt1 = float.Parse(_bottom);
                    doc.ShowTextAligned(new Paragraph("Page " + i + " of " + numberOfPages),
                                lf1, bt1, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
                    }
                    doc.Close();
            }
        }
    }
}
