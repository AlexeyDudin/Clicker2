using Clicker2.src.Model.URLs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clicker.src.Model
{
    public class SeleniumParams : INotifyPropertyChanged
    {
        private string paramName;
        
        private ObservableCollection<UrlClass> findUrl = new ObservableCollection<UrlClass>();
        private string request = "";

        private Browsers browser = Browsers.chrome;

        private string finderUrl = "";
        private string finderUrlEnd = "com";

        private ObservableCollection<UrlClass> explicitDomainList = new ObservableCollection<UrlClass>();

        private bool gotoPageAndRunNext = true;
        private bool gotoPageAndWait = false;
        private bool gotoPageAndRun = false;
        private int timeWork = 0;

        private IPAddress proxyIP = IPAddress.Parse("127.0.0.1");
        private IPEndPoint proxyPort = new IPEndPoint(IPAddress.Loopback, 80);
        private string proxyLogin = "";
        private string proxyPassword = "";

        private string userAgent = "";

        private bool useJS = true;
        private bool useCookie = true;

        private bool useTextLog = true;
        private bool useImageLog = false;
        private bool useVideoLog = false;

        public Browsers Browser { get => browser; set => browser = value; }
        public string FinderUrl { get => finderUrl; set => finderUrl = value; }
        public IPAddress ProxyIP { get => proxyIP; set => proxyIP = value; }
        public IPEndPoint ProxyPort { get => proxyPort; set => proxyPort = value; }
        public ObservableCollection<UrlClass> FindUrl { get => findUrl; set => findUrl = value; }
        public string Request { get => request; set => request = value; }
        public string ParamName { get => paramName; set => paramName = value; }
        public string ProxyLogin { get => proxyLogin; set => proxyLogin = value; }
        public string ProxyPassword { get => proxyPassword; set => proxyPassword = value; }
        public ObservableCollection<UrlClass> ExplicitDomain { get => explicitDomainList; set => explicitDomainList = value; }
        public bool GotoPageAndWait { get => gotoPageAndWait; set => gotoPageAndWait = value; }
        public bool GotoPageAndRun { get => gotoPageAndRun; set => gotoPageAndRun = value; }
        public int TimeWork { get => timeWork; set => timeWork = value; }
        public bool UseJS { get => useJS; set => useJS = value; }
        public bool UseCookie { get => useCookie; set => useCookie = value; }
        public string UserAgent { get => userAgent; set => userAgent = value; }
        public bool UseTextLog { get => useTextLog; set => useTextLog = value; }
        public bool UseImageLog { get => useImageLog; set => useImageLog = value; }
        public bool UseVideoLog { get => useVideoLog; set => useVideoLog = value; }
        public bool GotoPageAndRunNext { get => gotoPageAndRunNext; set => gotoPageAndRunNext = value; }
        public string FinderUrlEnd { get => finderUrlEnd; set => finderUrlEnd = value; }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

    }
}
