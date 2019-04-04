using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Data;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;


namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels
{
    internal class FormViewModel : BaseViewModel
    {
        #region Fields
        private readonly Process _process;
        #region Commands

        private RelayCommand<object> _killCommand;
        private RelayCommand<object> _toFolderCommand;
        private RelayCommand<object> _backCommand;

        private ObservableCollection<MyModule> _modules;
        private CollectionViewSource _modulesSource;
        private ObservableCollection<MyThread> _threads;
        private CollectionViewSource _threadsSource;
        #endregion

        #endregion

        #region Properties

        public ObservableCollection<MyModule> Modules
        {
            get { return _modules; }
            set
            {
                _modules = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<MyThread> Threads
        {
            get { return _threads; }
            set
            {
                _threads = value;
                OnPropertyChanged();
            }
        }
        public CollectionViewSource ModulesSource
        {
            get { return _modulesSource; }
            set
            {
                _modulesSource = value;
                OnPropertyChanged();
            }
        }
        public CollectionViewSource ThreadsSource
        {
            get { return _threadsSource; }
            set
            {
                _threadsSource = value;
                OnPropertyChanged();
            }
        }
        public FormViewModel(MyProcess p)
        {
            Modules = new ObservableCollection<MyModule>();
            ModulesSource = new CollectionViewSource { Source = Modules };
            Threads = new ObservableCollection<MyThread>();
            ThreadsSource = new CollectionViewSource { Source = Threads };
            _process = Process.GetProcessById(p.Id);
            var modules = _process.Modules;
            foreach (ProcessModule m in modules)
            {
                Modules.Add(new MyModule(m));
            }

            var threads = _process.Threads;
            foreach (ProcessThread t in threads)
            {
                Threads.Add(new MyThread(t));
            }
        }
        #region Commands

        public RelayCommand<object> KillCommand => _killCommand ?? (_killCommand = new RelayCommand<object>(Kill));
        public RelayCommand<object> ToFolderCommand => _toFolderCommand ?? (_toFolderCommand = new RelayCommand<object>(ToFolder));
        public RelayCommand<object> BackCommand => _backCommand ?? (_backCommand = new RelayCommand<object>(Result));

        #endregion

        #endregion

        private void Result(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.Main);
        }

        private void Kill(object obj)
        {
            _process.Kill();
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
        private void ToFolder(object obj)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.Arguments = System.IO.Directory.GetParent(_process.MainModule.FileName).FullName;
            startInfo.FileName = "explorer.exe";
            Process.Start(startInfo);
        }
    }
}