using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
        private ObservableCollection<User> _users;
        private Thread _workingThread;
        private CancellationToken _token;
        private CancellationTokenSource _tokenSource;
        private string _nameFilter = "";
        private string _surnameFilter = "";
        private string _emailFilter = "";
        private DateTime _fromBthFilter = DateTime.MinValue;
        private DateTime _toBthFilter = DateTime.Today;
        private string _sunSFilter = "";
        private string _chineseSFilter = "";
        private string _statusFilter = "";
        private string _bthFilter = "";
        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _deleteUsersCommand;
        private RelayCommand<object> _createUserCommand;

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public string NameFilter
        {
            get => _nameFilter;
            set
            {
                _nameFilter = value;
                UpdateFilter();
            }
        }

        public string SurnameFilter
        {
            get => _surnameFilter;
            set
            {
                _surnameFilter = value;
                UpdateFilter();
            }
        }

        public string EmailFilter
        {
            get => _emailFilter;
            set
            {
                _emailFilter = value;
                UpdateFilter();
            }
        }

        public DateTime FromBthFilter
        {
            get => _fromBthFilter;
            set
            {
                _fromBthFilter = value;
                UpdateFilter();
            }
        }

        public DateTime ToBthFilter
        {
            get => _toBthFilter;
            set
            {
                _toBthFilter = value;
                UpdateFilter();
            }
        }

        public string SunSFilter
        {
            get => _sunSFilter;
            set
            {
                _sunSFilter = value;
                UpdateFilter();
            }
        }

        public string ChineseSFilter
        {
            get => _chineseSFilter;
            set
            {
                _chineseSFilter = value;
                UpdateFilter();
            }
        }

        public string BthFilter
        {
            get => _bthFilter;
            set
            {
                _bthFilter = value;
                UpdateFilter();
            }
        }

        public string StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                UpdateFilter();
            }
        }

        private void UpdateFilter()
        {
            LoaderManager.Instance.ShowLoader();
            var users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
            var selectedUsers =
                (from u in users
                    where u.Name.Contains(NameFilter) &&
                          u.IsAdult.Contains(StatusFilter) &&
                          u.Surname.Contains(SurnameFilter) &&
                          u.Email.Contains(EmailFilter) &&
                          u.IsBirthday.Contains(BthFilter) &&
                          u.SunSign.Contains(SunSFilter) &&
                          u.ChineseSign.Contains(ChineseSFilter) &&
                          u.BirthDay >= FromBthFilter && u.BirthDay <= ToBthFilter
                    select u);

            Users = new ObservableCollection<User>(selectedUsers);
            LoaderManager.Instance.HideLoader();
        }

        public UserListViewModel(User p)
        {
            _users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;
            StartWorkingThread();
            StationManager.StopThreads += StopWorkingThread;
        }

        private void StartWorkingThread()
        {
            _workingThread = new Thread(WorkingThreadProcess);
            _workingThread.Start();
        }

        private void WorkingThreadProcess()
        {
            LoaderManager.Instance.ShowLoader();
            var users = _users.ToList();
            if (users.Count == 0)
            {
                var i = 0;
                while (i < 50)
                {
                    i++;
                    var user = new User(
                        User.Names[i],
                        User.Lastnames[i],
                        $"{User.Lastnames[i]}@gmail.com",
                        User.RandomDateBirth());
                    StationManager.DataStorage.AddUser(user);

                }
            }
            Users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
            LoaderManager.Instance.HideLoader();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumerable)
        {
            var col = new ObservableCollection<T>();
            foreach (var cur in enumerable)
            {
                col.Add(cur);
            }
            return col;
        }
        private async void Result(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var approve = await Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(2000);
                    StationManager.DataStorage.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                    return false;
                }
            }, _token);
            LoaderManager.Instance.HideLoader();
            if (approve)
                MessageBox.Show("Saved");
        }

        private void DeleteUsers(object obj)
        {
            System.Collections.IList collection = obj as System.Collections.IList;
            User[] a = new User[collection.Count];
            var i = 0;
            foreach (User u in collection.OfType<User>())
            {
                a[i] = u;
                i++;
            }

            for (var k = 0; k < i; k++)
            {
                Users.Remove(a[k]);
                StationManager.DataStorage.RemoveUser(a[k]);
            }
        }
        private void CreateUser(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.AddUser);
        }
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand<object>(Result));
        public ICommand CreateUserCommand => _createUserCommand ?? (_createUserCommand = new RelayCommand<object>(CreateUser));
        public ICommand DeleteUsersCommand => _deleteUsersCommand ?? (_deleteUsersCommand = new RelayCommand<object>(DeleteUsers));
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