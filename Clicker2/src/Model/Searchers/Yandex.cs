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
        private IWebDriver driver = null;

        public IWebDriver Driver { get => driver; set => driver = value; }

        public void ClickFindButton()
        {
            
        }

        public void ClickGotoNextPage()
        {
            
        }

        public void InsertSearch(string text)
        {
            
        }

        public Yandex(IWebDriver driver)
        {
            this.Driver = driver;
        }
    }
}
