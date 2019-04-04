using System.Diagnostics;
using System.IO;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Models
{
    class MyModule
    {
        public string Name { get; }
        public string Path { get; }
        public MyModule(ProcessModule r)
        {
            Name = r.ModuleName;
            Path = Directory.GetParent(r.FileName).FullName;
        }
    }
}
