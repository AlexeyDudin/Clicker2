using Clicker2.src.Model.Searchers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker2.src.Control.Searchers
{
    public class Yandex : ISearcher
    {
        public override void InsertSearch(string text)
        {
            throw new NotImplementedException();
        }

        public override void ClickGotoNextPage()
        {
            throw new NotImplementedException();
        }

        public override void ClickFindButton()
        {
            throw new NotImplementedException();
        }

        private void InitializeName()
        {
            base.Name = "yandex";
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

        public Yandex(IWebDriver driver)
        {
            this.Driver = driver;
            InitializeName();
        }

        public Yandex()
        {
            InitializeName();
        }
    }
}
