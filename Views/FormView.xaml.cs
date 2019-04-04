using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.ViewModels;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Views
{
    public partial class FormView : UserControl, INavigatable
    {
        public FormView(MyProcess p)
        {
            InitializeComponent();
            DataContext = new FormViewModel(p);
        }

        private void Modules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}