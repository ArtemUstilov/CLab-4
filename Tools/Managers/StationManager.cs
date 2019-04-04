using System;
using System.Windows;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Tools.Managers
{
    internal static class StationManager
    {
        public static event Action StopThreads;
        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}