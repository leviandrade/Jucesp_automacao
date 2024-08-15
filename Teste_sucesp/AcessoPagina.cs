using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using System.Drawing.Imaging;
using Teste_sucesp;

namespace Teste_sucesp
{
    public class AcessoPagina
    {
        public void Acessar()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", @"C:\Downloads");
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
            chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

            IWebDriver driver = new ChromeDriver(chromeOptions);

            driver.Navigate().GoToUrl("https://www.jucesponline.sp.gov.br/Login.aspx");

            driver.FindElement(By.Id("ctl00_cphContent_txtEmail")).SendKeys("47246155830");

            TentarSenhaECaptcha(driver);

            driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_txtPalavraChave")).SendKeys("47827672000139");
            driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_btPesquisar")).Click();

            TentarSenhaECaptcha(driver);

            IWebElement tabela = driver.FindElement(By.Id("ctl00_cphContent_gdvResultadoBusca_gdvContent"));

            TentarSenhaECaptcha(driver);

            tabela.FindElements(By.TagName("tr")).ElementAt(1).FindElements(By.TagName("a")).ElementAt(0).Click();

            var handle = driver.CurrentWindowHandle;

            driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_btnEmitir")).Click();


            string handlePopup = driver.WindowHandles.FirstOrDefault(p => p != handle);
            driver.SwitchTo().Window(handlePopup).Manage();
            var pagina = driver.PageSource;
            var teste = 1;
        }

        public void TentarSenhaECaptcha(IWebDriver driver)
        {
            if (IsElementPresent(driver, By.Id("ctl00_cphContent_txtSenha")))
            {
                driver.FindElement(By.Id("ctl00_cphContent_txtSenha")).Click();
                driver.FindElement(By.Id("ctl00_cphContent_txtSenha")).Clear();

                driver.FindElement(By.Id("ctl00_cphContent_txtSenha")).SendKeys("Levi2405");
            }

            //IWebElement tabelaLogin = driver.FindElement(By.Id("formBuscaAvancada")).FindElements(By.TagName("table")).ElementAt(0) ;
            //IWebElement imgCaptcha = tabelaLogin.FindElements(By.TagName("tr")).ElementAt(2).FindElements(By.TagName("img")).ElementAt(0);

            //    Point location = imgCaptcha.Location;
            //    var screenshot = (driver as ChromeDriver).GetScreenshot();

            //    using (MemoryStream stream = new(screenshot.AsByteArray))
            //    {
            //        using (Bitmap bitmap = new(stream))
            //        {
            //            RectangleF part = new(location.X, location.Y, imgCaptcha.Size.Width, imgCaptcha.Size.Height);
            //            using (Bitmap bn = bitmap.Clone(part, bitmap.PixelFormat))
            //            {
            //                bn.Save(arquivoCaptcha, System.Drawing.Imaging.ImageFormat.Png);
            //            }
            //        }
            //    }
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string base64string = js.ExecuteScript(@"
    var c = document.createElement('canvas');
    var ctx = c.getContext('2d');
    var img = $('.table01 img')[0];
    c.height=img.naturalHeight;
    c.width=img.naturalWidth;
    ctx.drawImage(img, 0, 0,img.naturalWidth, img.naturalHeight);
    var base64String = c.toDataURL();
    return base64String;
    ") as string;

            if (string.IsNullOrWhiteSpace(base64string))
                return;

            var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageName.png" + new Random().Next().ToString());
            var base64 = base64string.Split(',').Last();
            using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
            {
                using (var bitmap = new Bitmap(stream))
                {
                    bitmap.Save(filepath, ImageFormat.Png);
                }
            }

            var textoCapcha = new LerImagem()
                            .DeCaptcha(Image.FromFile(filepath));

            if (IsElementPresent(driver, By.Name("ctl00$cphContent$CaptchaControl1")))
                driver.FindElement(By.Name("ctl00$cphContent$CaptchaControl1")).SendKeys(textoCapcha);

            if (IsElementPresent(driver, By.Name("ctl00$cphContent$gdvResultadoBusca$CaptchaControl1")))
                driver.FindElement(By.Name("ctl00$cphContent$gdvResultadoBusca$CaptchaControl1")).SendKeys(textoCapcha);


            if (IsElementPresent(driver, By.Id("ctl00_cphContent_btEntrar")))
                driver.FindElement(By.Id("ctl00_cphContent_btEntrar")).Click();

            if (IsElementPresent(driver, By.Id("ctl00_cphContent_valSummary")))
            {
                TentarSenhaECaptcha(driver);
            }
        }
        private bool IsElementPresent(IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
