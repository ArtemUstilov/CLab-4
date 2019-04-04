using System;
using System.Diagnostics;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Models
{
    class MyThread
    {
        public int Id { get; }
        public string Status { get; }
        public DateTime Time { get; }
        public MyThread(ProcessThread r)
        {
            Id = r.Id;
            Status = r.ThreadState.ToString();
            Time = r.StartTime;
        }
    }
}
