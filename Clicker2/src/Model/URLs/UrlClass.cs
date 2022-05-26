using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clicker2.src.Model.URLs
{
    public class UrlClass: INotifyPropertyChanged
    {
        private string url = "";

        public string Url 
        {
            get => url;
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
