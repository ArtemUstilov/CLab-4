using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;


namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels
{
    internal class FormViewModel : BaseViewModel
    {
        #region Fields

        private User _person;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birth = new DateTime(1900, 1, 1);

        #region Commands

        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _backCommand;

        #endregion

        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _birth;
            set
            {
                _birth = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(Result,
                           o => CanExecuteCommand()));
            }
        }

        public RelayCommand<object> BackCommand => _backCommand ?? (_backCommand = new RelayCommand<object>(Result));

        #endregion

        #endregion

        private async void Result(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            Thread.Sleep(100);
            var approve = await Task.Run(() =>
            {
                try
                {
                    _person = new User(_name, _surname, _email, _birth);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                    return false;
                }
            }
            );
            if (approve)
            {
                StationManager.DataStorage.AddUser(_person);
                OnPropertyChanged();
                Thread.Sleep(200);
                NavigationManager.Instance.Navigate(ViewType.Main, _person);
            }
            LoaderManager.Instance.HideLoader();
        }
        private void Back(object obj)
        {
            Thread.Sleep(100);
            NavigationManager.Instance.Navigate(ViewType.Main);
        }
        public bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_name) &&
                   !string.IsNullOrWhiteSpace(_surname) &&
                   !string.IsNullOrWhiteSpace(_email) &&
                   !_birth.Equals(new DateTime(1900, 1, 1));
        }
    }
}