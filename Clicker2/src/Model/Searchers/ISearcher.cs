using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker2.src.Model.Searchers
{
    public interface ISearcher
    {
        void InsertSearch(string text);
        void ClickGotoNextPage();
        void ClickFindButton();

    }
}
