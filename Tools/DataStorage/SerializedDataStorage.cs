using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Models;
using KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.DataStorage
{
    internal class SerializedDataStorage:IDataStorage
    {
        private readonly List<User> _users;

        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<User>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = new List<User>();
            }
        }
        
   

        

        public void AddUser(User user)
        {
            _users.Add(user);
            SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _users.Remove(user);
            SaveChanges();
        }
        public List<User> UsersList
        {
            get { return _users.ToList(); }
        }

        public void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
        
    }
}

