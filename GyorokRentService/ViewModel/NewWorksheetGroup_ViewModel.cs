using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class NewWorksheetGroup_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;
        List<SQLConnectionLib.ServiceWorksheets> newWorksheets;

        private string _customerName;
        private DateTime _serviceStart;
        private ObservableCollection<GroupWorksheets> _groupWorksheets;
        private long _deposit;
        private GroupWorksheets _selectedWorksheet;        

        public string customerName
        {
            get
            {
                return _customerName;
            }

            set
            {
                if (_customerName == value)
                {
                    return;
                }

                _customerName = value;
                RaisePropertyChanged("customerName");
            }
        }
        public DateTime serviceStart
        {
            get
            {
                return _serviceStart;
            }

            set
            {
                if (_serviceStart == value)
                {
                    return;
                }

                _serviceStart = value;
                RaisePropertyChanged("serviceStart");
            }
        }
        public ObservableCollection<GroupWorksheets> groupWorksheets
        {
            get
            {
                return _groupWorksheets;
            }

            set
            {
                if (_groupWorksheets == value)
                {
                    return;
                }

                _groupWorksheets = value;
                RaisePropertyChanged("groupWorksheets");
            }
        }
        public long deposit
        {
            get
            {
                return _deposit;
            }

            set
            {
                if (_deposit == value)
                {
                    return;
                }

                _deposit = value;
                RaisePropertyChanged("deposit");
            }
        }
        public GroupWorksheets selectedWorksheet
        {
            get
            {
                return _selectedWorksheet;
            }

            set
            {
                if (_selectedWorksheet == value)
                {
                    return;
                }

                _selectedWorksheet = value;
                RaisePropertyChanged("selectedWorksheet");
            }
        }

        public ICommand deleteWorksheet { get { return new RelayCommand(deleteWorksheetExecute, () => true); } }
        void deleteWorksheetExecute()
        {
            ServiceWorksheets delWorksheet = new ServiceWorksheets();

            delWorksheet = newWorksheets.Find(nw => nw.yearCounter == selectedWorksheet.WorksheetIDNumber);
            newWorksheets.Remove(delWorksheet);
            AppMessages.NewWorksheetRemoved.Send(delWorksheet);
            groupWorksheets.Remove(selectedWorksheet);
        }
        public ICommand cancelGroup { get { return new RelayCommand(cancelGroupExecute, () => true); } }
        void cancelGroupExecute()
        {
            AppMessages.ServiceGroupClosed.Send(newWorksheets);
        }
        public ICommand acceptGroup { get { return new RelayCommand(acceptGroupExecute, () => true); } }
        void acceptGroupExecute()
        {
            Customers c = new Customers();
            ServiceGroups sg = new ServiceGroups();
            long cIDTemp;

            cIDTemp = newWorksheets[0].customerID;
            //c = SQLConnection.Execute.CustomersTable.Single(cust => cust.customerID == cIDTemp);
            c.serviceCounter += 1;

            sg.deposit = deposit;
            //SQLConnection.Execute.ServiceGroupsTable.AddObject(sg);
            //db.SaveChanges();
            foreach (ServiceWorksheets item in newWorksheets)
            {
                item.groupID = sg.serviceGroupID;
                //SQLConnection.Execute.ServiceWorksheetsTable.AddObject(item);
            }
            SQLConnection.Execute.SaveDb();
            foreach (ServiceWorksheets item in newWorksheets)
            {
                new Print.printService(item, Print.servicePrintType.bringIn);                
            }
            AppMessages.ServiceGroupClosed.Send(newWorksheets);
        }

        public NewWorksheetGroup_ViewModel(ref List<ServiceWorksheets> sw)
        {
            if (!this.IsInDesignMode)
            {
                //db = new dbGyorokEntities();
                groupWorksheets = new ObservableCollection<GroupWorksheets>();

                try
                {
                    newWorksheets = sw;

                    ServiceWorksheets swTemp = new ServiceWorksheets();
                    swTemp = sw[0];
                    //customerName = SQLConnection.Execute.CustomersTable.Single(c => c.customerID == swTemp.customerID).customerName;
                    serviceStart = (DateTime)swTemp.serviceStart;
                    foreach (ServiceWorksheets item in sw)
                    {
                        groupWorksheets.Add(new GroupWorksheets());
                        groupWorksheets[groupWorksheets.Count - 1].DeviceName = item.deviceName;
                        groupWorksheets[groupWorksheets.Count - 1].DeviceManufacturer = item.deviceManufacturer;
                        groupWorksheets[groupWorksheets.Count - 1].DeviceID = item.deviceID;
                        groupWorksheets[groupWorksheets.Count - 1].WorksheetIDNumber =  item.yearCounter;
                    }
                }
                catch (Exception)
                {
                }
            }            
        }
    }

    public class GroupWorksheets
    {
        private string deviceName;
        public string DeviceName
        {
            get { return deviceName; }
            set { deviceName = value; }
        }

        private string deviceManufacturer;
        public string DeviceManufacturer
        {
            get { return deviceManufacturer; }
            set { deviceManufacturer = value; }
        }

        private string deviceID;
        public string DeviceID
        {
            get { return deviceID; }
            set { deviceID = value; }
        }

        private int worksheetIDNumber;
        public int WorksheetIDNumber
        {
            get { return worksheetIDNumber; }
            set { 
                worksheetIDNumber = value;
                StrCnt = DateTime.Now.Year.ToString() + "/" + value.ToString("D4");
            }
        }

        private string strCnt;

        public string StrCnt
        {
            get { return strCnt; }
            set { strCnt = value; }
        }
        
    }
}
