////using System.Drawing;
////using Teste_sucesp;

////new LerImagem().DeCaptcha(Image.FromFile(@"C:\Users\Levi\Desktop\CaptchaImage (14).jpg"));



//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using System.Drawing;
//using System.Drawing.Imaging;
//using Teste_sucesp;

//var chromeOptions = new ChromeOptions();
//chromeOptions.AddUserProfilePreference("download.default_directory", @"C:\Downloads");
//chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
//chromeOptions.AddUserProfilePreference("download.directory_upgrade", true);
//chromeOptions.AddUserProfilePreference("plugins.always_open_pdf_externally", true);

//IWebDriver driver = new ChromeDriver(chromeOptions);

//driver.Navigate().GoToUrl("https://www.jucesponline.sp.gov.br/Login.aspx");

//driver.FindElement(By.Id("ctl00_cphContent_txtEmail")).SendKeys("47246155830");
//driver.FindElement(By.Id("ctl00_cphContent_txtSenha")).Click();
//driver.FindElement(By.Id("ctl00_cphContent_txtSenha")).Clear();

//driver.FindElement(By.Id("ctl00_cphContent_txtSenha")).SendKeys("Levi2405");



//    string arquivoCaptcha = Path.Combine(Path.GetTempPath(), "captcha.png");

////IWebElement tabelaLogin = driver.FindElement(By.Id("formBuscaAvancada")).FindElements(By.TagName("table")).ElementAt(0) ;
////IWebElement imgCaptcha = tabelaLogin.FindElements(By.TagName("tr")).ElementAt(2).FindElements(By.TagName("img")).ElementAt(0);

////    Point location = imgCaptcha.Location;
////    var screenshot = (driver as ChromeDriver).GetScreenshot();

////    using (MemoryStream stream = new(screenshot.AsByteArray))
////    {
////        using (Bitmap bitmap = new(stream))
////        {
////            RectangleF part = new(location.X, location.Y, imgCaptcha.Size.Width, imgCaptcha.Size.Height);
////            using (Bitmap bn = bitmap.Clone(part, bitmap.PixelFormat))
////            {
////                bn.Save(arquivoCaptcha, System.Drawing.Imaging.ImageFormat.Png);
////            }
////        }
////    }
//IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
//string base64string = js.ExecuteScript(@"
//    var c = document.createElement('canvas');
//    var ctx = c.getContext('2d');
//    var img = $('.table01 img')[0];
//    c.height=img.naturalHeight;
//    c.width=img.naturalWidth;
//    ctx.drawImage(img, 0, 0,img.naturalWidth, img.naturalHeight);
//    var base64String = c.toDataURL();
//    return base64String;
//    ") as string;

//var base64 = base64string.Split(',').Last();
//using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
//{
//    using (var bitmap = new Bitmap(stream))
//    {
//        var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageName.png");
//        bitmap.Save(filepath, ImageFormat.Png);
//    }
//}

//var textoCapcha = new LerImagem()
//                .DeCaptcha(Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageName.png")));

//driver.FindElement(By.Name("ctl00$cphContent$CaptchaControl1")).SendKeys(textoCapcha);


//driver.FindElement(By.Id("ctl00_cphContent_btEntrar")).Click();

//if(driver.FindElement(By.Id("ctl00_cphContent_valSummary")) != null)
//{

//}

//driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_txtPalavraChave")).SendKeys("47827672000139");
//driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_btPesquisar")).Click();

//IWebElement tabela = driver.FindElement(By.Id("ctl00_cphContent_gdvResultadoBusca_gdvContent"));
//tabela.FindElements(By.TagName("tr")).ElementAt(1).FindElements(By.TagName("a")).ElementAt(0).Click();

//var handle = driver.CurrentWindowHandle;

//driver.FindElement(By.Id("ctl00_cphContent_frmPreVisualiza_btnEmitir")).Click();


//string handlePopup = driver.WindowHandles.FirstOrDefault(p => p != handle);
//driver.SwitchTo().Window(handlePopup).Manage();
//var pagina = driver.PageSource;
//var teste = 1;

using Teste_sucesp;

new AcessoPagina().Acessar();