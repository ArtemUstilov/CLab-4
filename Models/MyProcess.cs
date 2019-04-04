using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace KMA.ProgrammingInCSharp2019.Practice7.UserList.Models
{
    public class MyProcess : INotifyPropertyChanged
    {
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _ramCounter;
        private double _cpuUsage;
        private double _ramUsage;
        public string StartTime { get; }
        public bool IsActive { get; }
        public int Id { get; }
        public string Name { get; }
        public string Path { get; }

        public double CpuUsage
        {
            get => _cpuUsage;
            set
            {
                if (value.Equals(_cpuUsage))
                    return;
                _cpuUsage = value;
                OnPropertyChanged("cpu");
            }
        }
        public double RamUsage
        {
            get => _ramUsage;
            set
            {
                if (value.Equals(_ramUsage))
                    return;
                _ramUsage = value;
                OnPropertyChanged("ram");
            }
        }
        public int NumOfThreads { get; }
        public MyProcess(Process p)
        {
            Name = p.ProcessName;
            Id = p.Id;
            IsActive = p.Responding;
            NumOfThreads = p.Threads.Count;

                try
                {
                    StartTime = p.StartTime.ToString(CultureInfo.CurrentCulture);
                    Path = p.MainModule.FileName;
                }
                catch
                {
                    // ignored
                }

                _cpuCounter = new PerformanceCounter(
                "Process", 
                "% Processor Time", 
                Name);
            _ramCounter = new PerformanceCounter(
                "Process",
                "Working Set - Private",
                Name);
        }

         public void UpdateData()
         {
             try
             {
                 double c = Math.Round(_cpuCounter.NextValue(), 2);
                 double r = Math.Round(_ramCounter.NextValue() / 1024 / 2042, 2);
                 CpuUsage = c > 100 ? 0 : c;
                 RamUsage = r > 100 ? 0 : r;
             }
             catch
             {
                 // ignored
             }
         }
        
         public event PropertyChangedEventHandler PropertyChanged;

         protected void OnPropertyChanged(string name)
         {
             PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
         }
    }
}