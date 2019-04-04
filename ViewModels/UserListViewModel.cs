using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Properties;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels
{
    class UserListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MyProcess> _process;
        private CollectionViewSource _viewSource;
        private Thread _workingThread;
        private BackgroundWorker _backgroundWorker;
        private BackgroundWorker _backgroundTimer;
        private BackgroundWorker _backgroundUpdater;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private bool _loaded;
        private int _time;
        private string _lastUpdate;
        private RelayCommand<object> _saveCommand;

        public ObservableCollection<MyProcess> MyProcesses
        {
            get { return _process; }
            set
            {
                _process = value;
                OnPropertyChanged();
            }
        }

        public CollectionViewSource ViewSource
        {
            get { return _viewSource; }
            set
            {
                _viewSource = value;
                OnPropertyChanged();
            }
        }

        public string LastUpdate
        {
            get => _lastUpdate;
            set
            {
                _lastUpdate = value;
                OnPropertyChanged();
            }
        }
        public UserListViewModel()
        {
            MyProcesses = new ObservableCollection<MyProcess>();
            ViewSource = new CollectionViewSource {Source = MyProcesses};
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            LastUpdate= "00:00";
            StartBackgroundWorker();
            StartWorkingThread();
            StationManager.StopThreads += StopWorkingThread;
            StationManager.StopThreads += StopBackgroundWorker;
        }

        private void StartWorkingThread()
        {
            _workingThread = new Thread(WorkingThreadProcess);
            _workingThread.Start();
        }

        private void WorkingThreadProcess()
        {
            var processes = Process.GetProcesses();
            LoaderManager.Instance.ShowLoader();
            foreach (var o in processes)
            {
                Application.Current.Dispatcher.Invoke(delegate { MyProcesses.Add(new MyProcess(o)); });
            }
            LoaderManager.Instance.HideLoader();
            _loaded = true;
        }

        private void StartBackgroundWorker()
        {
            _backgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            _backgroundUpdater = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            _backgroundTimer = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            _backgroundUpdater.DoWork += BackgroundUpdaterProcess;
            _backgroundWorker.DoWork += BackgroundWorkerProcess;
            _backgroundTimer.DoWork += BackgroundTimerProcess;
            _backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
            _backgroundWorker.RunWorkerAsync();
            _backgroundUpdater.RunWorkerAsync();
            _backgroundTimer.RunWorkerAsync();
        }

        private void BackgroundWorkerProcess(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var worker = (BackgroundWorker) sender;
            while(!_loaded)
                Thread.Sleep(2000);
            while (!worker.CancellationPending)
            {
                if (worker.CancellationPending)
                {
                    doWorkEventArgs.Cancel = true;
                    _tokenSource.Cancel();
                    break;
                }
                var excludedIDs = new List<int>(MyProcesses.Select(p => p.Id));
                var includedIds = Process.GetProcesses().Select(p => p.Id).ToList();
                var newProcesses = includedIds.Except(excludedIDs).ToList();
                var removedProcess = excludedIDs.Except(includedIds).ToList();
                foreach (var o in newProcesses)
                {
                    worker.ReportProgress(10, o);
                }
                foreach (var o in removedProcess)
                {
                    worker.ReportProgress(10, o);
                }

                _time = 0;
                Thread.Sleep(3000);
            }
        }
        private void BackgroundTimerProcess(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var worker = (BackgroundWorker)sender;
            while (!_loaded)
                Thread.Sleep(2000);
            while (!worker.CancellationPending)
            {
                if (worker.CancellationPending)
                {
                    doWorkEventArgs.Cancel = true;
                    _tokenSource.Cancel();
                    break;
                }
                LastUpdate = $"00:0{_time}";
                _time++;
                Thread.Sleep(1000);
            }
        }
        private void BackgroundUpdaterProcess(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var worker = (BackgroundWorker) sender;
            while (!_loaded)
                Thread.Sleep(2000);
            while (!worker.CancellationPending)
            {
                if (worker.CancellationPending)
                {
                    doWorkEventArgs.Cancel = true;
                    _tokenSource.Cancel();
                    break;
                }

                try
                {
                    UpdateData();
                }
                catch
                {
                    // ignored
                }

                Thread.Sleep(2000);
            }
        }

        private void UpdateData()
        {
            foreach (var p in MyProcesses)
            {
                p.UpdateData();
            }
        }

        private async void BackgroundWorkerOnProgressChanged(object sender,
            ProgressChangedEventArgs args)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    try
                    {
                        var p = Process.GetProcessById((int) args.UserState);
                        MyProcesses.Add(new MyProcess(p));
                    }
                    catch (ArgumentException)
                    {
                        try
                        {
                            MyProcesses.Remove(MyProcesses.First(i => i.Id == (int) args.UserState));
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                        
                });
            });
        }

        internal void StopBackgroundWorker()
        {
            if (_backgroundWorker == null) return;
            _backgroundWorker.CancelAsync();
            for (int i = 0; i < 4; i++)
            {
                if (_token.IsCancellationRequested)
                    break;
                Thread.Sleep(500);
            }

            _backgroundWorker.Dispose();
            _backgroundWorker = null;
        }


        private void GetInfo(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            System.Collections.IList collection = obj as System.Collections.IList;
            try
            {
                var selectedProcess = (collection ?? throw new InvalidOperationException()).OfType<MyProcess>().First();
                NavigationManager.Instance.Navigate(ViewType.SeeInfo, selectedProcess);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                LoaderManager.Instance.HideLoader();
                return;
            }
            LoaderManager.Instance.HideLoader();
        }

        public ICommand GetInfoCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(GetInfo));

        internal void StopWorkingThread()
        {
            _tokenSource.Cancel();
            _workingThread.Join(2000);
            _workingThread.Abort();
            _workingThread = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}