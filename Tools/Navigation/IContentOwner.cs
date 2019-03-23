using System.Windows.Controls;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Navigation
{
    internal interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
