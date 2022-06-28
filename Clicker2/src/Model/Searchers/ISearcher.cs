using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clicker2.src.Model.Searchers
{
    public abstract class ISearcher
    {
        private string name = "";


        private Random rnd = new Random();

        protected IWebDriver driver = null;

        public IWebDriver Driver { get => this.driver; set => this.driver = value; }

        public string Name { get => name; set => name = value; }

        public abstract void InsertSearch(string text);
        public abstract void ClickGotoNextPage();
        public abstract void ClickFindButton();

        public abstract bool IsFoundPrivacyTools();
        public abstract bool IsFoundCookiePolicies();
        public abstract void FocusOnCoockie();
        public abstract void ClickAcceptTerms();
    }
}
