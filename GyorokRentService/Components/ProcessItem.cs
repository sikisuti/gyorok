using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyorokRentService.Components
{
    public class ProcessItem : ViewModelBase
    {
        private string processName;
        public string ProcessName
        {
            get { return processName; }
            set
            {
                if (processName != value)
                {
                    processName = value;
                    RaisePropertyChanged("ProcessName");
                }                
            }
        }

        public ProcessItem(string name)
        {
            ProcessName = name;
        }
    }
}
