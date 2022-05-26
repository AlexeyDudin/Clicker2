using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker2.src.Model.Searchers
{
    public abstract class ISearcher
    {
        private string name = "";

        protected IWebDriver driver = null;

        public IWebDriver Driver { get => this.driver; set => this.driver = value; }

        public string Name { get => name; set => name = value; }

        public abstract void InsertSearch(string text);
        public abstract void ClickGotoNextPage();
        public abstract void ClickFindButton();

    }
}
