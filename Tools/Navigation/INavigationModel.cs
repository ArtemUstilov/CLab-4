using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation
{
    internal enum ViewType
    {
       AddUser,
        Main
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType, User user);
    }
}
