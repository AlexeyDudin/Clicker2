using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using Clicker2.src.Control;
using Clicker2.src.Model.Searchers;
using MahApps.Metro.Controls;

namespace Clicker2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private SeleniumParams selectedParam = new SeleniumParams();
        private ObservableCollection<BackgroundWorker> backgroundWorkerList = new ObservableCollection<BackgroundWorker>();
        private ObservableCollection<SeleniumParams> paramList = new ObservableCollection<SeleniumParams>();
        //private ObservableCollection<ISearcher> searcherList = null;
        private int counterParams = 1;

        private int maxProcCount = 1;

        public MainWindow()
        {
            InitializeComponent();

            Type baseType = typeof(ISearcher);
            List<Type> tmp = baseType.Assembly.ExportedTypes.Where(t => baseType.IsAssignableFrom(t)).ToList();
            tmp.Distinct();
            //searcherList = new ObservableCollection<ISearcher>();
            foreach (Type t in tmp)
            {
                if (!t.IsAbstract)
                {
                    var tmp1 = Activator.CreateInstance(t);
                    searcherCombobox.Items.Add((tmp1 as ISearcher).Name);
                }
            }
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

        public int MaxProcCount
        {
            get => maxProcCount;
            set
            {
                maxProcCount = value;
                OnPropertyChanged("MaxProcCount");
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
            if (selectedParam != null)
                ParamList.Remove(selectedParam);
        }

        private void DeleteGotoPageAndRun(object sender, RoutedEventArgs e)
        {
            SelectedParam.GotoPageAndRun = false;
        }

        private void DeleteGotoPageAndWait(object sender, RoutedEventArgs e)
        {
            SelectedParam.GotoPageAndWait = false;
        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            AddClick(this, new RoutedEventArgs());
            selectedParam = ParamList.First();
        }

        private void RunBackgroundWorker(object sender, DoWorkEventArgs e)
        {
            int runnedProcess = 0;
            ObservableCollection<SeleniumParams> paramList = e.Argument as ObservableCollection<SeleniumParams>;
            while (runnedProcess < paramList.Count)
            {
                if (backgroundWorkerList.Count <= maxProcCount)
                {
                    backgroundWorkerList.Add(InitThreadBackgroundWorker());
                    backgroundWorkerList.Last().RunWorkerAsync(paramList[runnedProcess]);
                    runnedProcess++;
                }
            }
            foreach (BackgroundWorker bg in backgroundWorkerList)
            {
                while (bg.IsBusy)
                { }
            }
        }

        private BackgroundWorker InitThreadBackgroundWorker()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += RunTask;
            worker.RunWorkerCompleted += ComleteTask;
            return worker;
        }

        private void ComleteTask(object sender, RunWorkerCompletedEventArgs e)
        {
            backgroundWorkerList.Remove(sender as BackgroundWorker);
        }

        private void RunTask(object sender, DoWorkEventArgs e)
        {
            SeleniumParams seleniumParams = e.Argument as SeleniumParams;
            ParamExecutor paramExecutor = new ParamExecutor(seleniumParams);
            paramExecutor.RunTask();
        }

        private BackgroundWorker InitMainBackgroundWorker()
        { 
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += RunBackgroundWorker;
            worker.RunWorkerCompleted += ComleteBackgroundWorker;
            return worker;
        }

        private void ComleteBackgroundWorker(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkButton.IsEnabled = true;
            MessageBox.Show(this, "Все задачи завершены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ButtonRunClick(object sender, RoutedEventArgs e)
        {
            RunWorkButton.IsEnabled = false;
            BackgroundWorker backgroundWorker = InitMainBackgroundWorker();
            backgroundWorker.RunWorkerAsync(paramList);
        }
    }
}
