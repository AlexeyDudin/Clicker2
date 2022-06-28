using Clicker.src.Logger;
using Clicker.src.Model;
using Clicker.src.Params;
using Clicker2.src.Model.Searchers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Clicker2.src.Control
{
    public class ParamExecutor
    {
        private ISearcher _searcher = null;
        private SeleniumParams seleniumParams = null;
        private LoggerWorker log = null;

        public SeleniumParams SeleniumParams { get => seleniumParams; set => seleniumParams = value; }

        public ParamExecutor(SeleniumParams seleniumParams)
        {
            this.SeleniumParams = seleniumParams;
        }

        private IWebDriver webDriver = null;

        private bool IsNetworkWork()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                ShowMessageBoxError("Отсутствует или ограниченно физическое подключение к сети\nПроверьте настройки вашего сетевого подключения");
                return false;
            }

            bool isConnected = false;
            try
            {
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.Connect(seleniumParams.FinderUrl.Replace("https:\\\\", "").Replace("http:\\\\", ""), 443); // google
                    isConnected = tcpClient.Connected;
                    tcpClient.Close();
                }
            }
            catch { }
            if (!isConnected)
            {
                ShowMessageBoxError("Нет подключения к интернету\nПроверьте ваш фаервол или настройки сетевого подключения");
                return false;
            }
            return true;
        }

        private void WaitForInternetConnection()
        {
            bool isShowedMessage = false;
            while (!IsNetworkWork())
            {
                if (!isShowedMessage)
                    ShowMessageBoxError("Нет подключения к интернету\nПроверьте ваш фаервол или настройки сетевого подключения");
                isShowedMessage = true;
                WaitForTime(60000);
            }
        }

        private void WaitForTime(int waitMillisecond)
        {
            var spin = new SpinWait();
            DateTime b = DateTime.Now;
            while (true)
            {
                if ((DateTime.Now - b).TotalMilliseconds >= waitMillisecond)
                    break;
                spin.SpinOnce();
            }
        }

        private void ShowMessageBoxError(string message)
        {
            var thread = new Thread(
            () =>
            {
                MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
            thread.Start();
            this.SeleniumParams.IsEnd = SeleniumStatusWork.Error;
        }

        private void FindSearcher()
        {
            Type baseType = typeof(ISearcher);
            List<Type> tmp = baseType.Assembly.ExportedTypes.Where(t => baseType.IsAssignableFrom(t)).ToList();
            tmp.Distinct();
            //searcherList = new ObservableCollection<ISearcher>();
            foreach (Type t in tmp)
            {
                if (!t.IsAbstract)
                {
                    var tmp1 = Activator.CreateInstance(t);
                    if ((tmp1 as ISearcher).Name.ToLower() == SeleniumParams.FinderUrl.ToLower())
                    {
                        _searcher = (tmp1 as ISearcher);
                    }
                }
            }
        }


        public void RunTask()
        {
            FindSearcher();
            log = new LoggerWorker(this.seleniumParams);
            InitializeWebDriver();

        }

        private void InitializeWebDriver()
        {
            //log = new LoggerWorker(seleniumParams);
            try
            {
                var chromeOptions = new ChromeOptions();
                if (seleniumParams.ProxyIP.IPAddress != IPAddress.Loopback)
                {
                    InitializeProxy();
                }
                if (!seleniumParams.UseJS)
                {
                    RemoveUseJS(ref chromeOptions);
                }
                if (!seleniumParams.UseCookie)
                {
                    RemoveUseCookie(ref chromeOptions);
                }
                if (!string.IsNullOrEmpty(seleniumParams.UserAgent))
                {
                    InjectUserAgent(ref chromeOptions);
                }

                if (seleniumParams.Browser == Browsers.mobile)
                {
                    ChromeMobileEmulationDeviceSettings mobile = new ChromeMobileEmulationDeviceSettings(seleniumParams.UserAgent);
                    try
                    {
                        mobile.Width = seleniumParams.ResX; //Properties.Settings.Default.BrowserSizeY;
                        mobile.Height = seleniumParams.ResY; //Properties.Settings.Default.BrowserSizeX;
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Не могу установить разрешение экрана для мобильного браузера!\nПожалуйста, проверьте настройки в файле settings.settings.\n{0}\n{1}", seleniumParams.ParamName, e.Message));
                    }
                    mobile.PixelRatio = 3;
                    mobile.UserAgent = seleniumParams.UserAgent;
                    chromeOptions.EnableMobileEmulation(mobile);
                }

                chromeOptions.AddExcludedArgument("enable-automation");
                chromeOptions.AddArgument("no-sandbox");
                chromeOptions.AddAdditionalCapability("useAutomationExtension", false);  // ("excludeSwitches", "enable-automation");

                //Load chrome binary
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.chromeBinaryPath))
                {
                    chromeOptions.BinaryLocation = Properties.Settings.Default.chromeBinaryPath;
                }

                //LoadExtensions
                //if (loadedCrxFiles.Count != 0)
                //{
                //    foreach (string crxPath in loadedCrxFiles)
                //    {
                //        chromeOptions.AddExtensions(crxPath);
                //    }
                //}

                //Выставляем разрешение согласно заданию
                //chromeOptions.AddArguments(string.Format("--window-size={0},{1}", seleniumParams.ResX, seleniumParams.ResY));

                //Запрещаем все загрузки
                chromeOptions.AddUserProfilePreference("download.default_directory", "NUL");
                //chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
                chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

                webDriver = new ChromeDriver(Properties.Settings.Default.ChromeDriver, chromeOptions, new TimeSpan(0, 0, seleniumParams.TimeToWaitSiteAndElement));
                webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, seleniumParams.TimeToWaitSiteAndElement);
                webDriver.Manage().Timeouts().PageLoad = new TimeSpan(0, 0, seleniumParams.TimeWork);
                //log.Add("Вебдрайвер запущен", webDriver);
                //Выставляем разрешение согласно заданию
                webDriver.Manage().Window.Size = new System.Drawing.Size(seleniumParams.ResX, seleniumParams.ResY);
            }
            catch (Exception e)
            {
                //log.Add(string.Format("Что-то пошло не так. Ошибка открытия браузера.\n{0}\n{1}", e.Message, seleniumParams.ParamName), webDriver);
                ShowMessageBoxError(string.Format("Что-то пошло не так! Ошибка открытия браузера!\n{0}\n{1}", e.Message, seleniumParams.ParamName));
                Exit();
                throw e;
            }

            try
            {
                DeleteCookie();
            }
            catch (Exception e)
            {
                log.Add(e.Message, webDriver);
            }

            try
            {
                //log.AddText("searcer initialized");
                WaitForInternetConnection();
                webDriver.Navigate().GoToUrl(seleniumParams.FinderUrl);
                //log.AddText("Перешли на страницу " + seleniumParams.FinderUrl);

                WaitRecapcha();

                WaitForTime(3000);
                CookieReset();
                log.Add("Браузер запущен", webDriver);
            }
            catch (WebDriverException ex)
            {
                ShowMessageBoxError(string.Format("{0}\nОшибка подключения к сайту! Проверьте настройки сети", seleniumParams.ParamName));
                log.Add("Ошибка подключения к сайту! Проверьте настройки сети", webDriver);
                webDriver.Close();
                throw ex;
            }
        }

        private bool IsFoundPrivacyTools()
        {
            return _searcher.IsFoundPrivacyTools();
        }

        private bool IsFoundCookiePolicies()
        {
            return _searcher.IsFoundCookiePolicies();
        }

        private bool CookieExist()
        {
            if (IsFoundPrivacyTools())
                return true;
            else if (IsFoundCookiePolicies())
                return true;
            else
                return false;
        }

        private void ScrollDown()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)webDriver;
            for (int i = 0; i < 5; i++)
            {
                jse.ExecuteScript("window.scrollBy(0,800)");
                Random rnd = new Random();
                WaitForTime(rnd.Next(1000, 3000));
            }
        }

        private void ClickAcceptTerms()
        {
            WaitForInternetConnection();
            _searcher.ClickAcceptTerms();
        }

        private void CookieReset()
        {
            if (CookieExist())
            {
                try
                {
                    _searcher.FocusOnCoockie();
                }
                catch
                { }
                ScrollDown();
                ClickAcceptTerms();
            }
        }

        private bool IsRecaptchaExist()
        {
            bool result = false;
            if (webDriver.PageSource.Contains("/recaptcha/"))
                result = true;
            return result;
        }

        private void WaitRecapcha()
        {
            WaitForInternetConnection();
            int tmpTime = seleniumParams.TimeToWaitRecaptcha;
            while (IsRecaptchaExist())
            {
                if (tmpTime == 0)
                    throw new NotFoundException(string.Format("Капча не решена в течении {0} секунд!", seleniumParams.TimeToWaitRecaptcha));
                WaitForTime(1000);
                tmpTime--;
            }
        }

        private void DeleteCookie()
        {
            throw new NotImplementedException();
        }

        private void Exit()
        {
            throw new NotImplementedException();
        }

        private void InjectUserAgent(ref ChromeOptions chromeOptions)
        {
            chromeOptions.AddArgument(string.Format("user-agent={0}", seleniumParams.UserAgent));
        }

        private void RemoveUseCookie(ref ChromeOptions chromeOptions)
        {
            chromeOptions.AddUserProfilePreference("network.cookie.cookieBehavior", 2);
        }

        private void RemoveUseJS(ref ChromeOptions chromeOptions)
        {
            chromeOptions.AddUserProfilePreference("profile.managed_default_content_settings.javascript", 2);
            chromeOptions.AddLocalStatePreference("profile.managed_default_content_settings.javascript", 2);
        }

        private void InitializeProxy()
        {
            var chromeOptions = new ChromeOptions();
            if (seleniumParams.ProxyType.Contains("http"))
            {
                if (string.IsNullOrWhiteSpace(seleniumParams.ProxyLogin))
                    chromeOptions.AddArgument(string.Format("--proxy-server={0}:{1}",
                                                        seleniumParams.ProxyIP.IPAddress.ToString(),
                                                        seleniumParams.ProxyPort.IPEndPoint.Port.ToString()));
                else
                    chromeOptions.AddArgument(string.Format("--proxy-server={0}:{1}@{2}:{3}",
                                                                                        HttpUtility.UrlEncode(seleniumParams.ProxyLogin),
                                                                                        HttpUtility.UrlEncode(seleniumParams.ProxyPassword),
                                                                                        seleniumParams.ProxyIP.IPAddress.ToString(),
                                                                                        seleniumParams.ProxyPort.IPEndPoint.Port.ToString()));

            }
            else if (seleniumParams.ProxyType.Contains("v5"))
            {
                if (string.IsNullOrEmpty(seleniumParams.ProxyLogin))
                    chromeOptions.AddArgument(string.Format("--proxy-server=socks5://{0}:{1}",
                                                        seleniumParams.ProxyIP.IPAddress.ToString(),
                                                        seleniumParams.ProxyPort.IPEndPoint.Port.ToString()));
                else
                    chromeOptions.AddArgument(string.Format("--proxy-server=socks5://{0}:{1}@{2}:{3}",
                                                            HttpUtility.UrlEncode(seleniumParams.ProxyLogin),
                                                            HttpUtility.UrlEncode(seleniumParams.ProxyPassword),
                                                            seleniumParams.ProxyIP.IPAddress.ToString(),
                                                            seleniumParams.ProxyPort.IPEndPoint.Port.ToString()));
            }
            else if (seleniumParams.ProxyType.Contains("v4"))
            {
                if (string.IsNullOrEmpty(seleniumParams.ProxyLogin))
                    chromeOptions.AddArgument(string.Format("--proxy-server=socks4://{0}:{1}",
                                                        seleniumParams.ProxyIP.IPAddress.ToString(),
                                                        seleniumParams.ProxyPort.IPEndPoint.Port.ToString()));
                else
                    chromeOptions.AddArgument(string.Format("--proxy-server=socks4://{0}:{1}@{2}:{3}",
                                                            HttpUtility.UrlEncode(seleniumParams.ProxyLogin),
                                                            HttpUtility.UrlEncode(seleniumParams.ProxyPassword),
                                                            seleniumParams.ProxyIP.IPAddress.ToString(),
                                                            seleniumParams.ProxyPort.IPEndPoint.Port.ToString()));
            }
        }
    }
}
