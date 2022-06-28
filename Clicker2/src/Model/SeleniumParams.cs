using Clicker.src.Params;
using Clicker2.src.Model.URLs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Clicker.src.Model
{
    [XmlRoot("SeleniumParam")]
    [Serializable]
    public class SeleniumParams : INotifyPropertyChanged
    {
        public SeleniumParams CloneParams(string paramName)
        {
            SeleniumParams clonedParams = new SeleniumParams();

            clonedParams.paramName = paramName;
            clonedParams.finderUrl = this.finderUrl;
            clonedParams.findUrl = new ObservableCollection<UrlClass>(this.findUrl);
            clonedParams.request = this.request;
            clonedParams.browser = this.browser;
            clonedParams.explicitDomainList = new ObservableCollection<UrlClass>(this.explicitDomainList);
            clonedParams.gotoPageAndRun = this.gotoPageAndRun;
            clonedParams.gotoPageAndRunNext = this.gotoPageAndRunNext;
            clonedParams.gotoPageAndWait = this.gotoPageAndWait;
            clonedParams.timeWork = this.timeWork;
            clonedParams.timeWorkAuto = this.timeWorkAuto;
            clonedParams.timeInSite = this.timeInSite;
            clonedParams.timeInSiteAuto = this.timeInSiteAuto;
            clonedParams.proxyIP.IPAddress = IPAddress.Parse(this.proxyIP.IPAddress.ToString());
            clonedParams.proxyPort.IPEndPoint = new IPEndPoint(proxyIP.IPAddress, this.proxyPort.IPEndPoint.Port);
            clonedParams.proxyLogin = this.proxyLogin;
            clonedParams.proxyPassword = this.proxyPassword;
            clonedParams.proxyType = this.proxyType;
            clonedParams.userAgent = this.userAgent;
            clonedParams.useJS = this.useJS;
            clonedParams.useCookie = this.useCookie;
            clonedParams.useTextLog = this.useTextLog;
            clonedParams.useImageLog = this.useImageLog;
            clonedParams.useVideoLog = this.useVideoLog;
            clonedParams.timeStart = DateTime.Parse(this.timeStart.ToString());
            clonedParams.timeToWaitSiteAndElement = this.timeToWaitSiteAndElement;
            clonedParams.timeToWaitNextPageMin = this.timeToWaitNextPageMin;
            clonedParams.timeToWaitNextPageMax = this.timeToWaitNextPageMax;
            clonedParams.searcherEnd = this.searcherEnd;
            clonedParams.timeToWaitRecaptcha = this.timeToWaitRecaptcha;
            clonedParams.resX = this.resX;
            clonedParams.resY = this.resY;
            clonedParams.isAll = this.isAll;
            clonedParams.minBypass = this.minBypass;
            clonedParams.maxByPass = this.maxByPass;
            clonedParams.isEnd = SeleniumStatusWork.NotRunning; //Склонированное задание должно отображаться новым заданием!
            return clonedParams;
        }

        private string paramName = "";

        private ObservableCollection<UrlClass> findUrl = new ObservableCollection<UrlClass>();
        private string request = "";

        private Browsers browser = (Browsers)Clicker2.Properties.Settings.Default.BrowserName;//BrowserEnums.Browsers.other;

        private string finderUrl = "http:\\\\google";

        private ObservableCollection<UrlClass> explicitDomainList = new ObservableCollection<UrlClass>();

        private bool gotoPageAndRunNext = Clicker2.Properties.Settings.Default.gotoPageAndGoNext;
        private bool gotoPageAndWait = Clicker2.Properties.Settings.Default.gotoPageAndWait;
        private bool gotoPageAndRun = Clicker2.Properties.Settings.Default.gotoPageAndStart;
        private int timeWork = 0;
        private bool timeWorkAuto = true;
        private int timeInSite = 0;
        private bool timeInSiteAuto = true;

        private ToSerializeIP proxyIP = new ToSerializeIP();

        private ToSerializePort proxyPort = new ToSerializePort();//new IPEndPoint(IPAddress.Loopback, 80);
        private string proxyLogin = "";
        private string proxyPassword = "";
        private string proxyType = "Без proxy";

        private string userAgent;

        private bool useJS = Clicker2.Properties.Settings.Default.useJS;
        private bool useCookie = Clicker2.Properties.Settings.Default.useCookie;

        private bool useTextLog = Clicker2.Properties.Settings.Default.saveFileLog;
        private bool useImageLog = Clicker2.Properties.Settings.Default.saveScreenLog;
        private bool useVideoLog = false;

        private DateTime timeStart = DateTime.Now;

        private int timeToWaitSiteAndElement = Clicker2.Properties.Settings.Default.timeToWaitSiteAndElement;

        private int timeToWaitNextPageMin = Clicker2.Properties.Settings.Default.timeToWaitNextPageMin;
        private int timeToWaitNextPageMax = Clicker2.Properties.Settings.Default.timeToWaitNextPageMax;

        private int timeToWaitRecaptcha = Clicker2.Properties.Settings.Default.timeToWaitRecaptcha;

        private string searcherEnd = Clicker2.Properties.Settings.Default.searcherEnd;

        private int resX = Clicker2.Properties.Settings.Default.BrowserSizeX;
        private int resY = Clicker2.Properties.Settings.Default.BrowserSizeY;

        private bool isAll = Clicker2.Properties.Settings.Default.useAllSite;
        private int minBypass = Clicker2.Properties.Settings.Default.minByPass;
        private int maxByPass = Clicker2.Properties.Settings.Default.maxByPass;

        private SeleniumStatusWork isEnd = SeleniumStatusWork.NotRunning;

        public Browsers Browser 
        { 
            get => browser;
            set
            {
                browser = value;
                OnPropertyChanged("Browser");
            }
        }
        public string FinderUrl 
        {
            get => finderUrl;
            set
            {
                finderUrl = value;
                OnPropertyChanged("FinderUrl");
            }
        }
        public ToSerializeIP ProxyIP
        { 
            get => proxyIP;
            set
            {
                proxyIP = value;
                OnPropertyChanged("ProxyIP");
            }
        }
        public ToSerializePort ProxyPort
        {
            get => proxyPort;
            set
            {
                proxyPort = value;
                OnPropertyChanged("ProxyPort");
            }
        }
        public ObservableCollection<UrlClass> FindUrl
        { 
            get => findUrl;
            set
            {
                findUrl = value;
                OnPropertyChanged("FindUrl");
            }
        }
        public string Request 
        {
            get => request;
            set
            {
                request = value;
                OnPropertyChanged("Request");
            }
        }
        public string ParamName 
        {
            get => paramName;
            set
            {
                paramName = value;
                OnPropertyChanged("ParamName");
            }
        }
        public string ProxyLogin
        {
            get => proxyLogin;
            set
            {
                proxyLogin = value;
                OnPropertyChanged("ProxyLogin");
            }
        }
        public string ProxyPassword
        {
            get => proxyPassword;
            set
            {
                proxyPassword = value;
                OnPropertyChanged("ProxyPassword");
            }
        }
        public ObservableCollection<UrlClass> ExplicitDomain
        { 
            get => explicitDomainList;
            set
            {
                explicitDomainList = value;
                OnPropertyChanged("ExplicitDomain");
            }
        }
        public bool GotoPageAndWait
        {
            get => gotoPageAndWait;
            set
            {
                gotoPageAndWait = value;
                OnPropertyChanged("GotoPageAndWait");
            }
        }
        public bool GotoPageAndRun
        {
            get => gotoPageAndRun;
            set
            {
                gotoPageAndRun = value;
                OnPropertyChanged("GotoPageAndRun");
            }
        }
        public int TimeWork 
        {
            get => timeWork;
            set
            {
                timeWork = value;
                OnPropertyChanged("TimeWork");
            }
        }
        public bool UseJS 
        { 
            get => useJS;
            set
            {
                useJS = value;
                OnPropertyChanged("UseJS");
            }
        }
        public bool UseCookie 
        {
            get => useCookie;
            set
            {
                useCookie = value;
                OnPropertyChanged("UseCookie");
            }
        }
        public string UserAgent 
        {
            get => userAgent;
            set
            {
                userAgent = value;
                OnPropertyChanged("UserAgent");
            }
        }
        public bool UseTextLog 
        {
            get => useTextLog;
            set
            {
                useTextLog = value;
                OnPropertyChanged("UseTextLog");
            }
        }
        public bool UseImageLog 
        { 
            get => useImageLog;
            set
            {
                useImageLog = value;
                OnPropertyChanged("UseImageLog");
            }
        }
        public bool UseVideoLog 
        { 
            get => useVideoLog;
            set
            {
                useVideoLog = value;
                OnPropertyChanged("UseVideoLog");
            }
        }
        public bool GotoPageAndRunNext 
        {
            get => gotoPageAndRunNext;
            set
            {
                gotoPageAndRunNext = value;
                OnPropertyChanged("GotoPageAndRunNext");
            }
        }
        public string ProxyType 
        { 
            get => proxyType;
            set
            {
                proxyType = value;
                OnPropertyChanged("ProxyType");
            }
        }
        public int TimeInSite 
        {
            get => timeInSite;
            set
            {
                timeInSite = value;
                OnPropertyChanged("TimeInSite");
            }
        }
        public DateTime TimeStart 
        {
            get => timeStart;
            set
            {
                timeStart = value;
                OnPropertyChanged("TimeStart");
            }
        }
        public int TimeToWaitSiteAndElement
        {
            get => timeToWaitSiteAndElement;
            set
            {
                timeToWaitSiteAndElement = value;
                OnPropertyChanged("TimeToWaitSiteAndElement");
            }
        }
        public bool TimeWorkAuto 
        {
            get => timeWorkAuto;
            set
            {
                timeWorkAuto = value;
                OnPropertyChanged("TimeWorkAuto");
            }
        }
        public bool TimeInSiteAuto 
        {
            get => timeInSiteAuto;
            set
            {
                timeInSiteAuto = value;
                OnPropertyChanged("TimeInSiteAuto");
            }
        }
        public int TimeToWaitNextPageMin 
        { 
            get => timeToWaitNextPageMin;
            set
            {
                timeToWaitNextPageMin = value;
                OnPropertyChanged("TimeToWaitNextPageMin");
            }
        }
        public int TimeToWaitNextPageMax 
        {
            get => timeToWaitNextPageMax;
            set
            {
                timeToWaitNextPageMax = value;
                OnPropertyChanged("TimeToWaitNextPageMax");
            }
        }
        public int TimeToWaitRecaptcha 
        {
            get => timeToWaitRecaptcha;
            set
            {
                timeToWaitRecaptcha = value;
                OnPropertyChanged("TimeToWaitRecaptcha");
            }
        }
        public string SearcherEnd 
        {
            get => searcherEnd;
            set
            {
                searcherEnd = value;
                OnPropertyChanged("SearcherEnd");
            }
        }
        public int ResX
        { 
            get => resX;
            set
            {
                resX = value;
                OnPropertyChanged("ResX");
            }
        }
        public int ResY 
        {
            get => resY;
            set
            {
                resY = value;
                OnPropertyChanged("ResY");
            }
        }
        public bool IsAll
        {
            get => isAll;
            set
            {
                isAll = value;
                OnPropertyChanged("IsAll");
            }
        }
        public int MinBypass 
        {
            get => minBypass;
            set
            {
                minBypass = value;
                OnPropertyChanged("MinBypass");
            }
        }
        public int MaxByPass 
        {
            get => maxByPass;
            set
            {
                maxByPass = value;
                OnPropertyChanged("MaxByPass");
            }
        }
        public SeleniumStatusWork IsEnd 
        {
            get => isEnd;
            set
            {
                isEnd = value;
                OnPropertyChanged("IsEnd");
            }
        }

        public SeleniumParams()
        {
            proxyIP.IPAddress = IPAddress.Loopback;
            proxyPort.IPEndPoint = new IPEndPoint(IPAddress.Loopback, 80);
        }

        public class ToSerializeIP
        {
            [XmlElement(ElementName = "IPAddress")]
            public string IPAddressAsString
            {
                get { return IPAddress != null ? IPAddress.ToString() : null; }
                set
                {
                    IPAddress a;
                    if (value != null && IPAddress.TryParse(value, out a))
                        IPAddress = a;
                    else
                        IPAddress = null;

                }
            }
            [XmlIgnore]
            public IPAddress IPAddress { get; set; }
        }

        public class ToSerializePort
        {
            [XmlElement(ElementName = "IPEndPoint")]
            public string IPEndPointAsString
            {
                get 
                {
                    return IPEndPoint != null ? IPEndPoint.Port.ToString() : null; }
                set
                {
                    if (value != null)
                    {
                        string[] ep = value.Split(':');
                        if (ep.Length != 2)
                            IPEndPoint = new IPEndPoint(IPEndPoint.Address, Int32.Parse(ep[0]));
                        else
                        {
                            IPAddress ab = null;
                            IPAddress.TryParse(ep[0], out ab);
                            IPEndPoint = new IPEndPoint(ab.Address, Int32.Parse(ep[1]));
                        }
                        //IPEndPoint.Address = ab;
                        //IPEndPoint.Port = Int32.Parse(ep[1]);
                    }
                    else
                        IPEndPoint = null;
                }
            }
            [XmlIgnore]
            public IPEndPoint IPEndPoint { get; set; }
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
