


using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text;

public class teste
{
    public void achar()
    {
        var src = @"C:\Downloads\VisualizaTicket.pdf";
        var pdfDocument = new PdfDocument(new PdfReader(src));
        var strategy = new LocationTextExtractionStrategy();
        string socios = "";
        bool paginaSocio = false;
        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); ++i)
        {
            var page = pdfDocument.GetPage(i);
            string text = PdfTextExtractor.GetTextFromPage(page, strategy);
            string processed = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
            if (paginaSocio || processed.Contains("TITULAR / SÓCIOS / DIRETORIA"))
            {
                string textoPagina = processed;
                if (!paginaSocio)
                    textoPagina = processed.Split("TITULAR / SÓCIOS / DIRETORIA")[1];

                if (textoPagina.Contains("FIM DAS INFORMAÇÕES"))
                {
                    socios += textoPagina.Split("FIM DAS INFORMAÇÕES")[0];
                    break;
                }
                else
                {
                    socios += textoPagina;
                }
                paginaSocio = true;
            }
        }
        pdfDocument.Close();
        var foundIndexes = new List<int>();
        var cpfs = new List<string>();
        long t1 = DateTime.Now.Ticks;
        for (int i = socios.IndexOf("CPF:"); i > -1; i = socios.IndexOf("CPF:", i + 1))
        {
            // for loop end when i=-1 ('a' not found)
            cpfs.Add(socios.Substring(i + 5, 14));
            foundIndexes.Add(i);
        }
        var teste = "";
    }
}