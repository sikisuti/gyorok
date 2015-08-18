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
    class ServiceSum_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;

        private ObservableCollection<ServiceSum> _worksheets;
        private ServiceSum _selectedWorksheet;
        private long _materialCost;
        private long _totalCost;
        private bool _mustSave;
        private string _comment;
        private string _searchYear;
        private string _searchCode;
        private string _searchName;
        private string _searchDevice;
        private ObservableCollection<Parts> _builtInParts;
        private bool _serviceFinished;
        private bool _showArchive;        

        public ObservableCollection<ServiceSum> worksheets
        {
            get
            {
                return _worksheets;
            }

            set
            {
                if (_worksheets == value)
                {
                    return;
                }

                _worksheets = value;
                RaisePropertyChanged("worksheets");
            }
        }
        public ServiceSum selectedWorksheet
        {
            get
            {
                return _selectedWorksheet;
            }

            set
            {
                long tempCost;

                if (_selectedWorksheet == value || value == null)
                {
                    return;
                }

                tempCost = 0;
                var inserts = SQLConnection.Execute.PartsTable.Where(p => p.serviceID == value.worksheetID).Select(p => new { p.partPrice, p.partQuantity });
                foreach (var item in inserts)
                {
                    tempCost += item.partPrice * (long)item.partQuantity;
                }
                materialCost = tempCost;

                if (value.serviceCost == null)
                {
                    totalCost = materialCost;
                }
                else
                {
                    totalCost = (long)Math.Round((long)value.serviceCost * (1 - value.discount) + materialCost);
                }
                comment = value.wsComment;

                if (new List<long>(){5, 6}.Contains(value.statusID) && !value.isPaid)
                {
                    serviceFinished = true;
                }
                else
                {
                    serviceFinished = false;
                }
                
                _selectedWorksheet = value;
                RaisePropertyChanged("selectedWorksheet");
                RefreshParts();
            }
        }
        public long materialCost
        {
            get
            {
                return _materialCost;
            }

            set
            {
                if (_materialCost == value)
                {
                    return;
                }

                _materialCost = value;
                RaisePropertyChanged("materialCost");
            }
        }
        public long totalCost
        {
            get
            {
                return _totalCost;
            }

            set
            {
                if (_totalCost == value)
                {
                    return;
                }

                _totalCost = value;
                RaisePropertyChanged("totalCost");
            }
        }
        public bool mustSave
        {
            get
            {
                return _mustSave;
            }

            set
            {
                if (_mustSave == value)
                {
                    return;
                }

                _mustSave = value;
                RaisePropertyChanged("mustSave");
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
        public string searchYear
        {
            get
            {
                return _searchYear;
            }

            set
            {
                if (_searchYear == value)
                {
                    return;
                }

                _searchYear = value;
                RaisePropertyChanged("searchYear");
            }
        }
        public string searchCode
        {
            get
            {
                return _searchCode;
            }

            set
            {
                if (_searchCode == value)
                {
                    return;
                }

                _searchCode = value;
                RaisePropertyChanged("searchCode");
            }
        }
        public string searchName
        {
            get
            {
                return _searchName;
            }

            set
            {
                if (_searchName == value)
                {
                    return;
                }

                _searchName = value;
                RaisePropertyChanged("searchName");
            }
        }
        public string searchDevice
        {
            get
            {
                return _searchDevice;
            }

            set
            {
                if (_searchDevice == value)
                {
                    return;
                }

                _searchDevice = value;
                RaisePropertyChanged("searchDevice");
            }
        }
        public ObservableCollection<Parts> builtInParts
        {
            get
            {
                return _builtInParts;
            }

            set
            {
                if (_builtInParts == value)
                {
                    return;
                }

                _builtInParts = value;
                RaisePropertyChanged("builtInParts");
            }
        }
        public bool serviceFinished
        {
            get
            {
                return _serviceFinished;
            }

            set
            {
                if (_serviceFinished == value)
                {
                    return;
                }

                _serviceFinished = value;
                RaisePropertyChanged("serviceFinished");
            }
        }
        public bool showArchive
        {
            get
            {
                return _showArchive;
            }

            set
            {
                if (_showArchive == value)
                {
                    return;
                }

                _showArchive = value;
                RaisePropertyChanged("showArchive");

                RefreshServiceList();
            }
        }

        public ICommand print { get { return new RelayCommand(printExecute, () => true); } }
        void printExecute()
        {
            if (selectedWorksheet.isPaid)
            {
                new Print.printService(selectedWorksheet, Print.servicePrintType.takesAway);                
            }
            else
            {
                new Print.printService(selectedWorksheet, Print.servicePrintType.quotation); 
            }
        }
        public ICommand contentChanged { get { return new RelayCommand(contentChangedExecute, () => true); } }
        void contentChangedExecute()
        {
            mustSave = true;
        }
        public ICommand save { get { return new RelayCommand(saveExecute, () => true); } }
        void saveExecute()
        {
            long tempID;
            //string tempComment;
            //tempComment = selectedWorksheet.wsComment;
            tempID = selectedWorksheet.worksheetID;
            ServiceWorksheets sw = new ServiceWorksheets();
            sw = SQLConnection.Execute.ServiceWorksheetsTable.Single(s => s.worksheetID == tempID);
            sw.comment = comment;
            SQLConnection.Execute.SaveDb();
            RefreshServiceList();
            
            selectedWorksheet = SQLConnection.Execute.ServiceSumView.Single(ss => ss.worksheetID == tempID);
            mustSave = false;
        }
        public ICommand searchTextChanged { get { return new RelayCommand(searchTextChangedExecute, () => true); } }
        void searchTextChangedExecute()
        {
            ServiceListUpdate();
        }
        public ICommand quotAccept { get { return new RelayCommand(quotAcceptExecute, () => true); } }
        void quotAcceptExecute()
        {
            ServiceWorksheets acceptedWS = new ServiceWorksheets();
            List<Parts> tempParts = new List<Parts>();
            bool waitForPart = false;
            
            acceptedWS = SQLConnection.Execute.ServiceWorksheetsTable.Single(sws => sws.worksheetID == selectedWorksheet.worksheetID);
            acceptedWS.quotAccepted = true;
            
            tempParts = SQLConnection.Execute.PartsTable.Where(p => p.serviceID == acceptedWS.worksheetID).ToList();
            foreach (var item in tempParts)
            {
                if (item.mustOrder)
                {
                    waitForPart = true;
                    break;
                }
            }

            if (waitForPart)
            {
                acceptedWS.statusID = 4;
            }
            else
            {
                acceptedWS.statusID = 1;
            }

            SQLConnection.Execute.SaveDb();
            RefreshServiceList();
        }
        public ICommand quotReject { get { return new RelayCommand(quotRejectExecute, () => true); } }
        void quotRejectExecute()
        {
            ServiceWorksheets rejectedWS = new ServiceWorksheets();

            rejectedWS = SQLConnection.Execute.ServiceWorksheetsTable.Single(sws => sws.worksheetID == selectedWorksheet.worksheetID);
            rejectedWS.quotAccepted = false;
            rejectedWS.quotRequested = true;
            //rejectedWS.inQuotMode = false;
            rejectedWS.statusID = 7;

            SQLConnection.Execute.SaveDb();
            RefreshServiceList();
        }
        public ICommand takeAway { get { return new RelayCommand(takeAwayExecute, () => true); } }
        void takeAwayExecute()
        {
            ServiceWorksheets takedAwayWS = new ServiceWorksheets();

            if (selectedWorksheet.worksheetID != 0)
            {
                takedAwayWS = SQLConnection.Execute.ServiceWorksheetsTable.Single(sws => sws.worksheetID == selectedWorksheet.worksheetID);
                takedAwayWS.serviceEnd = DateTime.Now;
                takedAwayWS.isPaid = true;
                if (takedAwayWS.serviceCost == null)
                {
                    takedAwayWS.serviceCost = 0;
                }

                SQLConnection.Execute.SaveDb();
                new Print.printService(takedAwayWS, Print.servicePrintType.takesAway);
                RefreshServiceList();
            }
        }
        public ICommand backToService { get { return new RelayCommand(backToServiceExecute, () => true); } }
        void backToServiceExecute()
        {
            ServiceWorksheets sentWS = new ServiceWorksheets();

            sentWS = SQLConnection.Execute.ServiceWorksheetsTable.Single(sws => sws.worksheetID == selectedWorksheet.worksheetID);
            sentWS.inQuotMode = false;
            sentWS.quotAccepted = false;
            if (sentWS.quotRequested)
            {
                sentWS.statusID = 2;
            }
            else
            {
                sentWS.statusID = 1;
            }

            SQLConnection.Execute.SaveDb();
            RefreshServiceList();
        }
        public ICommand showDetails { get { return new RelayCommand(showDetailsExecute, () => true); } }
        void showDetailsExecute()
        {
            GyorokRentService.View.ServiceDetailsWindow win = new GyorokRentService.View.ServiceDetailsWindow(selectedWorksheet.customerID);
            win.ShowDialog();
        }

        public ServiceSum_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                selectedWorksheet = new ServiceSum();
                selectedWorksheet.isPaid = false;
                selectedWorksheet.inQuotMode = false;
                selectedWorksheet.quotAccepted = false;
                searchCode = string.Empty;
                searchYear = DateTime.Now.Year.ToString();
                searchName = string.Empty;
                searchDevice = string.Empty;
                RefreshServiceList();
                mustSave = false;

                AppMessages.ServiceGroupClosed.Register(this, s => RefreshServiceList());
                AppMessages.ServiceWorksheetsChanged.Register(this, rc => RefreshServiceList());
                AppMessages.PartsChanged.Register(this, s => RefreshParts());
            }
            else
            {
                mustSave = true;
                serviceFinished = true;
            }
        }

        public void RefreshServiceList()
        {
            ServiceListUpdate();
        }

        private void ServiceListUpdate()
        {
            List<ServiceSum> foundYear = new List<ServiceSum>();
            int year;
            int code;

            if (showArchive)
            {
                foundYear = SQLConnection.Execute.ServiceSumView.Where(ss => true).ToList(); 
            }
            else
            {
                foundYear = SQLConnection.Execute.ServiceSumView.Where(ss => !ss.isPaid).ToList(); 
            }

            if (searchYear != string.Empty)
            {
                int.TryParse(searchYear, out year);
                foundYear = foundYear.Where(ss1 => ss1.serviceStart.Year == year).ToList();
            }

            if (searchCode != string.Empty)
            {
                int.TryParse(searchCode, out code);
                foundYear = foundYear.Where(f1 => f1.yearCounter == code).ToList();
            }

            if (searchName != string.Empty)
            {
                foundYear = foundYear.Where(f2 => f2.customerName.StartsWith(searchName, true, null)).ToList();
            }

            if (searchDevice != string.Empty)
            {
                foundYear = foundYear.Where(f3 => f3.deviceName.StartsWith(searchDevice, true, null)).ToList();
            }

            foundYear.Sort((x, y) => DateTime.Compare(y.serviceStart, x.serviceStart));
            
            worksheets = new ObservableCollection<ServiceSum>(foundYear);
            //if (worksheets.Count > 0)
            //{
            //    selectedWorksheet = worksheets[0];
            //}
        }

        private void RefreshParts()
        {
            if (selectedWorksheet.worksheetID != null)
            {
                builtInParts = new ObservableCollection<Parts>(SQLConnection.Execute.PartsTable.Where(sp => sp.serviceID == selectedWorksheet.worksheetID).ToList()); 
            }
        }
    }
}
