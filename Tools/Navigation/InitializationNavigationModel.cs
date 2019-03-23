using System;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Views;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {
            
        }
   
        protected override void InitializeView(ViewType viewType, User user)
        {
            switch (viewType)
            {
      
                case ViewType.Main:
                    if(ViewsDictionary.ContainsKey(viewType))
                        ViewsDictionary[viewType] = new UserListView(user);
                    else
                        ViewsDictionary.Add(viewType, new UserListView(user));
                    break;
                case ViewType.AddUser:
                    ViewsDictionary.Add(viewType, new FormView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
