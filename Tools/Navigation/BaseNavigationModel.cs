using System.Collections.Generic;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation
{
    internal abstract class BaseNavigationModel : INavigationModel
    {
        private readonly IContentOwner _contentOwner;
        private readonly Dictionary<ViewType, INavigatable> _viewsDictionary;

        protected BaseNavigationModel(IContentOwner contentOwner)
        {
            _contentOwner = contentOwner;
            _viewsDictionary = new Dictionary<ViewType, INavigatable>();
        }

        protected IContentOwner ContentOwner => _contentOwner;

        protected Dictionary<ViewType, INavigatable> ViewsDictionary => _viewsDictionary;

        public void Navigate(ViewType viewType, MyProcess process)
        {
            if (!ViewsDictionary.ContainsKey(viewType) || viewType == ViewType.SeeInfo)
                InitializeView(viewType, process);
            ContentOwner.ContentControl.Content = ViewsDictionary[viewType];
        }   

        protected abstract void InitializeView(ViewType viewType, MyProcess user);

    }
}
