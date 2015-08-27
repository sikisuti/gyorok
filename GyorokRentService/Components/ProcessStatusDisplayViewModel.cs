using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GyorokRentService.Components
{
    public sealed class ProcessStatusDisplayViewModel : ViewModelBase
    {
        private static ProcessStatusDisplayViewModel _instance = new ProcessStatusDisplayViewModel();
        public static ProcessStatusDisplayViewModel Instance { get { return _instance; } }

        private ObservableCollection<ProcessItem> processList = new ObservableCollection<ProcessItem>();
        public ObservableCollection<ProcessItem> ProcessList
        {
            get { return processList; }
            set
            {
                if (processList != value)
                {
                    ProcessList = value;
                    RaisePropertyChanged("ProcessList");
                }
            }
        }

        private ProcessStatusDisplayViewModel()
        {
        }
    }
}
