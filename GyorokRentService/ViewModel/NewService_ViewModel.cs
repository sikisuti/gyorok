using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GyorokRentService.View;
using System.Collections.ObjectModel;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    public class NewService_ViewModel : ViewModelBase
    {

        private ServiceWorksheets _newService;
        public ServiceWorksheets newService
        {
            get { return _newService; }
            set
            {
                if (_newService != value)
                {
                    _newService = value;
                    RaisePropertyChanged("newService");
                }
            }
        }
        
        long _customerID;
        int yearCnt;

        private string _denomination;                
        private string _faultDescription;
        private string _manufacturer;
        private string _IDNumber;
        //private int _worksheetCount;
        private ObservableCollection<SQLConnectionLib.ErrorTypes> _errorType;
        private SQLConnectionLib.ErrorTypes _selectedErrorType;
        private bool _hasWarranty;
        private float _discount;
        private bool _requestQuot;
        private string _plusParts;
        private string _worksheetCounter;
        private long? _maxCost;
        private string _warrantyID;
        private string _serial;
        private string _comment;
        private DateTime? _buyDate;        

        public string denomination
        {
            get
            {
                return _denomination;
            }

            set
            {
                if (_denomination == value)
                {
                    return;
                }

                _denomination = value;
                RaisePropertyChanged("denomination");
            }
        }
        public string faultDescription
        {
            get
            {
                return _faultDescription;
            }

            set
            {
                if (_faultDescription == value)
                {
                    return;
                }

                _faultDescription = value;
                RaisePropertyChanged("faultDescription");
            }
        }
        public string manufacturer
        {
            get
            {
                return _manufacturer;
            }

            set
            {
                if (_manufacturer == value)
                {
                    return;
                }

                _manufacturer = value;
                RaisePropertyChanged("manufacturer");
            }
        }
        public string IDNumber
        {
            get
            {
                return _IDNumber;
            }

            set
            {
                if (_IDNumber == value)
                {
                    return;
                }

                _IDNumber = value;
                RaisePropertyChanged("IDNumber");
            }
        }
        //public int worksheetCount
        //{
        //    get
        //    {
        //        return _worksheetCount;
        //    }

        //    set
        //    {
        //        if (_worksheetCount == value)
        //        {
        //            return;
        //        }
                
        //        _worksheetCount = value;
        //        RaisePropertyChanged("worksheetCount");
                
        //    }
        //}
        public ObservableCollection<SQLConnectionLib.ErrorTypes> errorType
        {
            get
            {
                return _errorType;
            }

            set
            {
                if (_errorType == value)
                {
                    return;
                }

                _errorType = value;
                RaisePropertyChanged("errorType");
            }
        }
        public SQLConnectionLib.ErrorTypes selectedErrorType
        {
            get
            {
                return _selectedErrorType;
            }

            set
            {
                if (_selectedErrorType == value)
                {
                    return;
                }

                _selectedErrorType = value;
                RaisePropertyChanged("selectedErrorType");
            }
        }
        public bool hasWarranty
        {
            get
            {
                return _hasWarranty;
            }

            set
            {
                if (_hasWarranty == value)
                {
                    return;
                }

                _hasWarranty = value;
                RaisePropertyChanged("hasWarranty");
            }
        }
        public float discount
        {
            get
            {
                return _discount;
            }

            set
            {
                if (_discount == value)
                {
                    return;
                }

                _discount = value;
                RaisePropertyChanged("discount");
            }
        }
        public bool requestQuot
        {
            get
            {
                return _requestQuot;
            }

            set
            {
                if (_requestQuot == value)
                {
                    return;
                }

                _requestQuot = value;
                RaisePropertyChanged("requestQuot");
            }
        }
        public string plusParts
        {
            get
            {
                return _plusParts;
            }

            set
            {
                if (_plusParts == value)
                {
                    return;
                }

                _plusParts = value;
                RaisePropertyChanged("plusParts");
            }
        }
        public string worksheetCounter
        {
            get
            {
                return _worksheetCounter;
            }

            set
            {
                if (_worksheetCounter == value)
                {
                    return;
                }

                _worksheetCounter = value;
                RaisePropertyChanged("worksheetCounter");
            }
        }
        public long? maxCost
        {
            get
            {
                return _maxCost;
            }

            set
            {
                if (_maxCost == value)
                {
                    return;
                }

                _maxCost = value;
                RaisePropertyChanged("maxCost");
            }
        }
        public string warrantyID
        {
            get
            {
                return _warrantyID;
            }

            set
            {
                if (_warrantyID == value)
                {
                    return;
                }

                _warrantyID = value;
                RaisePropertyChanged("warrantyID");
            }
        }
        public string serial
        {
            get
            {
                return _serial;
            }

            set
            {
                if (_serial == value)
                {
                    return;
                }

                _serial = value;
                RaisePropertyChanged("serial");
            }
        }
        public string comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                _comment = value;
                RaisePropertyChanged("comment");
            }
        }
        public DateTime? buyDate
        {
            get
            {
                return _buyDate;
            }

            set
            {
                if (_buyDate == value)
                {
                    return;
                }

                _buyDate = value;
                RaisePropertyChanged("buyDate");
            }
        }

        public ICommand addWorksheet { get { return new RelayCommand(addWorksheetExecute, () => true); } }
        void addWorksheetExecute()
        {
            if (_customerID != null && denomination != null && denomination != string.Empty)
            {
                ServiceWorksheets newWorksheet = new ServiceWorksheets();
                newWorksheet.customerID = _customerID;
                newWorksheet.serialID = serial;
                newWorksheet.deviceID = IDNumber;
                newWorksheet.deviceManufacturer = manufacturer;
                newWorksheet.deviceName = denomination;
                if (hasWarranty)
                {
                    newWorksheet.hasWarranty = true;
                    newWorksheet.warrantyNumber = warrantyID;
                }
                else
                {
                    newWorksheet.hasWarranty = false;
                    newWorksheet.warrantyNumber = string.Empty;
                }
                newWorksheet.errorTypeID = selectedErrorType.errorTypeID;
                newWorksheet.errorDescription = faultDescription;
                if (requestQuot)
                {
                    newWorksheet.statusID = 2;
                    newWorksheet.quotRequested = true;
                }
                else
                {
                    newWorksheet.statusID = 1;
                    newWorksheet.quotRequested = false;
                }
                newWorksheet.serviceStart = DateTime.Now;
                newWorksheet.isPaid = false;
                newWorksheet.comment = comment;
                newWorksheet.discount = discount / 100;
                newWorksheet.inQuotMode = false;
                newWorksheet.quotAccepted = false;
                newWorksheet.plusParts = plusParts;
                newWorksheet.buyDate = buyDate;
                newWorksheet.warrantyNumber = warrantyID;
                newWorksheet.maxCost = maxCost;
                GetWorksheetIDNumber();
                newWorksheet.yearCounter = yearCnt;

                //SQLConnection.Execute.ServiceWorksheetsTable.AddObject(newWorksheet);
                SQLConnection.Execute.SaveDb();

                new Print.printService(newWorksheet, Print.servicePrintType.bringIn);

                denomination = string.Empty;
                manufacturer = string.Empty;
                IDNumber = string.Empty;
                serial = string.Empty;
                requestQuot = false;
                hasWarranty = false;
                warrantyID = string.Empty;
                buyDate = null;
                faultDescription = string.Empty;
                maxCost = null;
                plusParts = string.Empty;
                comment = string.Empty;

                GetWorksheetIDNumber();
            }
        }

        public NewService_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                AppMessages.CustomerToSelect.Register(this, c => 
                {
                    _customerID = c.id;

                    if (c.defaultDiscount != null)
                    {
                        discount = (float)c.defaultDiscount * 100; 
                    }
                    else
                    {
                        discount = 0;
                    }
                });
                AppMessages.ServiceWorksheetsChanged.Register(this, rc => GetWorksheetIDNumber());
                hasWarranty = false;
                requestQuot = false;
                //errorType = new ObservableCollection<SQLConnectionLib.ErrorTypes>(SQLConnection.Execute.ErrorTypesTable);
                selectedErrorType = errorType[0];
                GetWorksheetIDNumber();
            }
            else
            {
                hasWarranty = true;
            }
        }

        private void GetWorksheetIDNumber()
        {
            int lastYear = 0;

            try
            {
                //lastYear = (SQLConnection.Execute.ServiceWorksheetsTable.Select(s => s.serviceStart).Max()).Year;
            }
            catch (Exception)
            {
                lastYear = 0;
            }

            if (DateTime.Now.Year > lastYear)
            {
                yearCnt = 0;
            }
            else
            {
                //yearCnt = SQLConnection.Execute.ServiceWorksheetsTable.Where(wo => wo.serviceStart.Year == DateTime.Now.Year).Select(wo => wo.yearCounter).Max() + 1;
                if (yearCnt == null)
                {
                    yearCnt = 0;
                }
            }

            worksheetCounter = DateTime.Now.Year.ToString() + "/" + (yearCnt).ToString("D4");
        }
    }
}
