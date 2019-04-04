using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation
{
    internal enum ViewType
    {
       SeeInfo,
        Main
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType, MyProcess user);
    }
}
