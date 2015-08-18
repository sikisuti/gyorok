using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GyorokRentService.ViewModel;
using System.Windows;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class NewTool_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;

        private string _newToolName;
        private string _newToolManufacturer;
        private string _newToolIdNumber;
        private string _newToolSerial;
        private string _newRentPrice;
        private DateTime _fromDate;
        private long _defaultDeposit;
        private bool _contentOK = false;
               
        public string newToolName
        {
            get
            {
                return _newToolName;
            }

            set
            {
                if (_newToolName == value)
                {
                    return;
                }
                
                _newToolName = value;
                RaisePropertyChanged("newToolName");

                CheckContent();
            }
        }
        public string newToolManufacturer
        {
            get
            {
                return _newToolManufacturer;
            }

            set
            {
                if (_newToolManufacturer == value)
                {
                    return;
                }

                _newToolManufacturer = value;
                RaisePropertyChanged("newToolManufacturer");
            }
        }
        public string newToolIdNumber
        {
            get
            {
                return _newToolIdNumber;
            }

            set
            {
                if (_newToolIdNumber == value)
                {
                    return;
                }
                _newToolIdNumber = value;
                RaisePropertyChanged("newToolIdNumber");

                CheckContent();
            }
        }
        public string newToolSerial
        {
            get
            {
                return _newToolSerial;
            }

            set
            {
                if (_newToolSerial == value)
                {
                    return;
                }

                _newToolSerial = value;
                RaisePropertyChanged("newToolSerial");
            }
        }
        public string newRentPrice
        {
            get
            {
                return _newRentPrice;
            }

            set
            {
                if (_newRentPrice == value)
                {
                    return;
                }

                _newRentPrice = value;
                RaisePropertyChanged("newRentPrice");

                CheckContent();
            }
        }
        public DateTime fromDate
        {
            get
            {
                return _fromDate;
            }

            set
            {
                if (_fromDate == value)
                {
                    return;
                }

                _fromDate = value;
                RaisePropertyChanged("fromDate");
            }
        }
        public long defaultDeposit
        {
            get
            {
                return _defaultDeposit;
            }

            set
            {
                if (_defaultDeposit == value)
                {
                    return;
                }

                _defaultDeposit = value;
                RaisePropertyChanged("defaultDeposit");
            }
        }
        public bool contentOK
        {
            get
            {
                return _contentOK;
            }

            set
            {
                if (_contentOK == value)
                {
                    return;
                }

                _contentOK = value;
                RaisePropertyChanged("contentOK");
            }
        }

        public ICommand insertNewTool { get { return new RelayCommand(insertNewToolExecute, () => true); } }
        void insertNewToolExecute()
        {
            long l;
            SQLConnectionLib.Tools newTool = new SQLConnectionLib.Tools();
            newTool.toolName = newToolName;
            newTool.toolManufacturer = newToolManufacturer;
            newTool.IDNumber = newToolIdNumber;
            newTool.serialNumber = newToolSerial;
            newTool.fromDate = fromDate;
            long.TryParse(newRentPrice, out l);
            newTool.rentPrice = l;
            newTool.toolStatusID = 1;
            newTool.rentCounter = 0;
            newTool.defaultDeposit = defaultDeposit;
            SQLConnection.Execute.ToolsTable.AddObject(newTool);
            SQLConnection.Execute.SaveDb();
            AppMessages.ToolToSelect.Send(newTool);
        }

        public NewTool_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                //db = new dbGyorokEntities();

                fromDate = DateTime.Today;
                defaultDeposit = 0;
            }
        }

        private void CheckContent() {
            if (newToolName != null && newToolName != string.Empty &&
                newToolIdNumber != null && newToolIdNumber != string.Empty &&
                newRentPrice != null && newRentPrice != string.Empty)
            {
                contentOK = true;
            }
            else
            {
                contentOK = false;
            }
        }
    }
}
