using Clicker2.src.Model.Searchers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker2.src.Control.Searchers
{
    public class DuckDuckGo : ISearcher
    {
        public override void ClickFindButton()
        {
            throw new NotImplementedException();
        }

        public override void ClickGotoNextPage()
        {
            throw new NotImplementedException();
        }

        public override void InsertSearch(string text)
        {
            throw new NotImplementedException();
        }

        private void InitializeName()
        {
            base.Name = "duckduckgo";
        }

        public override bool IsFoundPrivacyTools()
        {
            throw new NotImplementedException();
        }

        public override bool IsFoundCookiePolicies()
        {
            throw new NotImplementedException();
        }

        public override void FocusOnCoockie()
        {
            throw new NotImplementedException();
        }

        public override void ClickAcceptTerms()
        {
            throw new NotImplementedException();
        }

        public DuckDuckGo()
        {
            InitializeName();
        }

        public DuckDuckGo(IWebDriver driver)
        {
            InitializeName();
            this.Driver = driver;
        }
    }
}
