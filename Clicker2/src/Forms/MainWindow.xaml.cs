using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Clicker.src.Model;
using MahApps.Metro.Controls;

namespace Clicker2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private SeleniumParams selectedParam = new SeleniumParams();
        private ObservableCollection<SeleniumParams> paramList = new ObservableCollection<SeleniumParams>();
        private int counterParams = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        public ObservableCollection<SeleniumParams> ParamList
        { 
            get => paramList;
            set
            {
                paramList = value;
                OnPropertyChanged("ParamList");
            }
        }
        public SeleniumParams SelectedParam 
        {
            get => selectedParam;
            set
            {
                selectedParam = value;
                OnPropertyChanged("SelectedParam");
            }
        }

        private void OnPropertyChanged(string name) 
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void AddClick(object sender, RoutedEventArgs e)
        {
            SeleniumParams newParam = new SeleniumParams();
            newParam.ParamName = "Задание № " + counterParams;
            counterParams++;
            ParamList.Add(newParam);
            OnPropertyChanged("ParamList");
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
