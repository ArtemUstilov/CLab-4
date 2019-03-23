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

        protected IContentOwner ContentOwner
        {
            get { return _contentOwner; }
        }

        protected Dictionary<ViewType, INavigatable> ViewsDictionary
        {
            get { return _viewsDictionary; }
        }

        public void Navigate(ViewType viewType, User user)
        {
            //if (!ViewsDictionary.ContainsKey(viewType))
                InitializeView(viewType, user);
            ContentOwner.ContentControl.Content = ViewsDictionary[viewType];
        }   

        protected abstract void InitializeView(ViewType viewType, User user);

    }
}
