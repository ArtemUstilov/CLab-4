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
        
        protected override void InitializeView(ViewType viewType, MyProcess process)
        {
            switch (viewType)
            {
      
                case ViewType.Main:
                    if(ViewsDictionary.ContainsKey(viewType))
                        ViewsDictionary[viewType] = new UserListView();
                    else
                        ViewsDictionary.Add(viewType, new UserListView());
                    break;
                case ViewType.SeeInfo:
                    if (ViewsDictionary.ContainsKey(viewType))
                        ViewsDictionary[viewType] = new FormView(process);
                    else
                        ViewsDictionary.Add(viewType, new FormView(process));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
