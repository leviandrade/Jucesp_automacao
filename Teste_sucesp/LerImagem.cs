using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Teste_sucesp
{
    internal class LerImagem
    {
        public string OCR(Bitmap bmp)
        {
            using (TesseractEngine engine = new TesseractEngine(@"tessdata", "eng", EngineMode.Default))
            {
                engine.SetVariable("tessedit_char_whitelist", "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                engine.SetVariable("tessedit_unrej_any_wd", true);

                bmp.Save("file.png", System.Drawing.Imaging.ImageFormat.Png); // ImageFormat.Jpeg, etc
                using var img = Tesseract.Pix.LoadFromFile(@"C:\Users\Levi\source\repos\Teste_sucesp\Teste_sucesp\bin\Debug\net6.0\file.png");

                using (var page = engine.Process(img, PageSegMode.SingleLine))
                {
                    return page.GetText();
                }
            }
        }
        public string DeCaptcha(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bmp = bmp.Clone(new Rectangle(0, 0, img.Width, img.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Erosion erosion = new Erosion();
            Dilatation dilatation = new Dilatation();
            Invert inverter = new Invert();
            ColorFiltering cor = new ColorFiltering();
            cor.Blue = new AForge.IntRange(200, 255);
            cor.Red = new AForge.IntRange(200, 255);
            cor.Green = new AForge.IntRange(200, 255);
            Opening open = new Opening();
            BlobsFiltering bc = new BlobsFiltering() { MinHeight = 10 };
            Closing close = new Closing();
            GaussianSharpen gs = new GaussianSharpen();
            ContrastCorrection cc = new ContrastCorrection();
            FiltersSequence seq = new FiltersSequence(gs, inverter, open, inverter, bc, inverter, open, cc, cor, bc, inverter);
            var imagem = seq.Apply(bmp);
             return OCR(imagem);
        }
    }
   
}
