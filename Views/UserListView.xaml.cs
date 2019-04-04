using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Views
{
    public partial class UserListView : INavigatable
    {
        public UserListView()
        {
            InitializeComponent();
            DataContext = new UserListViewModel();
        }
    }
}
