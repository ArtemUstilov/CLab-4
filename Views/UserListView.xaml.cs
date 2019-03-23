using System;
using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Views
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView : UserControl, INavigatable
    {
        public UserListView(User u)
        {
            InitializeComponent();
            DataContext = new UserListViewModel(u);
        }
    }
}
