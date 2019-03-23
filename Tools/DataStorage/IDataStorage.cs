using System.Collections.Generic;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.DataStorage
{
    internal interface IDataStorage
    {

        void AddUser(User user);
        void RemoveUser(User user);
        List<User> UsersList { get; }
        void SaveChanges();
    }
}
