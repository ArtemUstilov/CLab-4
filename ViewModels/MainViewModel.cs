//using System.Windows;
//using System.Windows.Input;
//using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools;
//using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;

//namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels
//{
//    internal class MainViewModel : BaseViewModel
//    {
//        private Visibility _menuVisibility = Visibility.Collapsed;
//        private ICommand _showMenuCommand;
//        private ICommand _closeCommand;

//        public Visibility MenuVisibility
//        {
//            get { return _menuVisibility; }
//            private set
//            {
//                _menuVisibility = value; 
//                OnPropertyChanged();
//            }
//        }



//        private void ShowMenuImplementation(object obj)
//        {
//            MenuVisibility = _menuVisibility==Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
//        }

       

//        private void CloseImplementation(object obj)
//        {
//            StationManager.CloseApp();
//        }
//    }
//}
