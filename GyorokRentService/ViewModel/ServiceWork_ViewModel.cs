using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GyorokRentService.View;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class ServiceWork_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;
        long _serviceID;
        ServiceWorksheets modifiedWorksheet = new ServiceWorksheets();

        private ObservableCollection<SQLConnectionLib.ServiceSum> _worksheetList;
        private SQLConnectionLib.ServiceSum _selectedWorksheet;
        private string _error;
        private string _comment;
        private long? _laborFee;
        private ObservableCollection<Parts> _builtInParts;
        private Parts _selectedPart;
        private long _totalCost;
        private float _discount;
        private bool _mustSave;
        private bool _inQuotMode;
        private bool _finishable;        

        public ObservableCollection<SQLConnectionLib.ServiceSum> worksheetList
        {
            get
            {
                return _worksheetList;
            }

            set
            {
                if (_worksheetList == value)
                {
                    return;
                }

                _worksheetList = value;
                RaisePropertyChanged("worksheetList");
            }
        }
        public SQLConnectionLib.ServiceSum selectedWorksheet
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

                if (value == null)
                {
                    builtInParts.Clear();
                    return;
                }

                modifiedWorksheet = SQLConnection.Execute.ServiceWorksheetsTable.Single(sw => sw.worksheetID == value.worksheetID);
                                
                error = value.errorDescription; 
                comment = value.wsComment; 
                laborFee = value.serviceCost;
                
                _serviceID = value.worksheetID;
                discount = (float)value.discount * 100;
                RefreshParts();
                if (value.inQuotMode)
                {
                    inQuotMode = true;
                }
                else
                {
                    inQuotMode = false;
                }            

                _selectedWorksheet = value;
                RaisePropertyChanged("selectedWorksheet");
                
                FinishableCheck();    
            }
        }
        public string error
        {
            get
            {
                return _error;
            }

            set
            {
                if (_error == value)
                {
                    return;
                }

                _error = value;
                RaisePropertyChanged("error");
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
        public long? laborFee
        {
            get
            {
                return _laborFee;
            }

            set
            {
                if (_laborFee == value)
                {
                    return;
                }

                _laborFee = value;
                RaisePropertyChanged("laborFee");
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
        public Parts selectedPart
        {
            get
            {
                return _selectedPart;
            }

            set
            {
                if (_selectedPart == value)
                {
                    return;
                }

                _selectedPart = value;
                RaisePropertyChanged("selectedPart");
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

                if (value)
                {
                    finishable = false;
                }
                else
                {
                    FinishableCheck();
                }

                _mustSave = value;
                RaisePropertyChanged("mustSave");
            }
        }
        public bool inQuotMode
        {
            get
            {
                return _inQuotMode;
            }

            set
            {
                if (_inQuotMode == value)
                {
                    return;
                }

                _inQuotMode = value;
                RaisePropertyChanged("inQuotMode");
            }
        }
        public bool finishable
        {
            get
            {
                return _finishable;
            }

            set
            {
                if (_finishable == value)
                {
                    return;
                }

                _finishable = value;
                RaisePropertyChanged("finishable");
            }
        }

        public ICommand openAddPartWindow { get { return new RelayCommand(openAddPartWindowExecute, () => true); } }
        void openAddPartWindowExecute()
        {
            NewPart npWindow = new NewPart();
            npWindow.Show();
        }
        public ICommand openModifyPartWindow { get { return new RelayCommand(openModifyPartWindowExecute, () => true); } }
        void openModifyPartWindowExecute()
        {
            NewPart npWindow;

            if (selectedPart != null)
            {
                if (inQuotMode)
                {
                    npWindow = new NewPart(selectedPart, false); 
                }
                else
                {
                    npWindow = new NewPart(selectedPart, true); 
                }
                npWindow.Show(); 
            }
        }
        public ICommand commentChanged { get { return new RelayCommand(commentChangedExecute, () => true); } }
        void commentChangedExecute()
        {
            mustSave = true;
        }
        public ICommand laborFeeChanged { get { return new RelayCommand(laborFeeChangedExecute, () => true); } }
        void laborFeeChangedExecute()
        {
            modifiedWorksheet.serviceCost = laborFee;
            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
        }
        public ICommand discountChanged { get { return new RelayCommand(discountChangedExecute, () => true); } }
        void discountChangedExecute()
        {
            modifiedWorksheet.discount = discount / 100;
            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
        }
        public ICommand textChanged { get { return new RelayCommand(textChangedExecute, () => true); } }
        void textChangedExecute()
        {
            mustSave = true;
        }
        public ICommand removePart { get { return new RelayCommand(removePartExecute, () => true); } }
        void removePartExecute()
        {
            Parts p = new Parts();
            
            p = SQLConnection.Execute.PartsTable.Single( pa => pa.partID == selectedPart.partID);
            SQLConnection.Execute.PartsTable.DeleteObject(p);
            SQLConnection.Execute.SaveDb();
            RefreshParts();
        }
        public ICommand serviceDone { get { return new RelayCommand(serviceDoneExecute, () => true); } }
        void serviceDoneExecute()
        {
            ServiceWorksheets sw = new ServiceWorksheets();
            sw = SQLConnection.Execute.ServiceWorksheetsTable.Single(w => w.worksheetID == selectedWorksheet.worksheetID);
            sw.statusID = 5;
            if (sw.serviceCost == null)
            {
                sw.serviceCost = 0;
            }
            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
        }
        public ICommand save { get { return new RelayCommand(saveExecute, () => true); } }
        void saveExecute()
        {
            long tempSS;
            tempSS = selectedWorksheet.worksheetID;
            ServiceWorksheets sw = new ServiceWorksheets();
            sw = SQLConnection.Execute.ServiceWorksheetsTable.Single(w => w.worksheetID == tempSS);
            sw.serviceCost = laborFee;
            sw.discount = discount / 100;
            sw.errorDescription = error;
            sw.comment = comment;
            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
            selectedWorksheet = worksheetList.Single(wl => wl.worksheetID == tempSS);
            mustSave = false;
        }
        public ICommand notFixable { get { return new RelayCommand(notFixableExecute, () => true); } }
        void notFixableExecute()
        {
            ServiceWorksheets sw = new ServiceWorksheets();
            sw = SQLConnection.Execute.ServiceWorksheetsTable.Single(w => w.worksheetID == selectedWorksheet.worksheetID);
            sw.statusID = 6;
            if (sw.serviceCost == null)
            {
                sw.serviceCost = 0;
            }
            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
        }
        public ICommand quotCreated { get { return new RelayCommand(quotCreatedExecute, () => true); } }
        void quotCreatedExecute()
        {
            long tempSS;
            tempSS = selectedWorksheet.worksheetID;
            ServiceWorksheets sw = new ServiceWorksheets();
            sw = SQLConnection.Execute.ServiceWorksheetsTable.Single(w => w.worksheetID == tempSS);
            sw.statusID = 3;
            sw.inQuotMode = true;
            if (sw.serviceCost == null)
            {
                sw.serviceCost = 0;
            }
            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
            selectedWorksheet = worksheetList.Single(wl => wl.worksheetID == tempSS);
        }
        public ICommand quotUndo { get { return new RelayCommand(quotUndoExecute, () => true); } }
        void quotUndoExecute()
        {
            long tempSS;
            tempSS = selectedWorksheet.worksheetID;
            ServiceWorksheets sw = new ServiceWorksheets();
            sw = SQLConnection.Execute.ServiceWorksheetsTable.Single(w => w.worksheetID == tempSS);
            if (sw.quotRequested)
            {
                sw.statusID = 2;
            }
            else
            {
                sw.statusID = 1;
            }
            sw.quotAccepted = false;
            sw.inQuotMode = false;
            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
            selectedWorksheet = worksheetList.Single(wl => wl.worksheetID == tempSS);
        }
        public ICommand showDetails { get { return new RelayCommand(showDetailsExecute, () => true); } }
        void showDetailsExecute()
        {
            ServiceDetailsWindow win = new ServiceDetailsWindow(selectedWorksheet.customerID);
            win.ShowDialog();
        }

        public ServiceWork_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                RefreshGroupList();
                mustSave = false;

                AppMessages.PartAdded.Register(this, PartAddedActions);
                AppMessages.PartModified.Register(this, PartModifiedActions);
                AppMessages.ServiceGroupClosed.Register(this, s => RefreshGroupList());
                AppMessages.PartsChanged.Register(this, p => RefreshParts());
                AppMessages.ServiceWorksheetsChanged.Register(this, rc => RefreshGroupList());
            }
            else
            {
                mustSave = true;
                inQuotMode = false;
                finishable = true;
            }
        }

        public void RefreshGroupList()
        {
            long prevSelWS = 0;

            if (selectedWorksheet != null)
            {
                prevSelWS = selectedWorksheet.worksheetID; 
            }

            //if (db != null)
            //{
            //    db.Dispose(); 
            //}
            //db = new dbGyorokEntities();
            
            worksheetList = new ObservableCollection<SQLConnectionLib.ServiceSum>(SQLConnection.Execute.ServiceSumView.Where(s => new List<long>() { 1, 2, 3, 4, 7 }.Contains(s.statusID) && !s.isPaid).ToList());
            if (worksheetList.Count > 0)
            {
                if (prevSelWS != 0 && worksheetList.Select(wsl => wsl.worksheetID).Contains(prevSelWS))
                {
                    selectedWorksheet = SQLConnection.Execute.ServiceSumView.Single(ws => ws.worksheetID == prevSelWS);
                }
                else
                {
                    selectedWorksheet = worksheetList[0];
                }
            }
        }

        private void RefreshParts()
        {
            if (_serviceID != 0)
            {
                builtInParts = new ObservableCollection<Parts>(SQLConnection.Execute.PartsTable.Where(sp => sp.serviceID == _serviceID).ToList());
                CalcTotalCost(); 
            }
        }

        private void PartAddedActions(Parts p) 
        {
            if (p.partID == null || p.partID == 0)
            {
                p.serviceID = _serviceID;
                p.partID = 0;
                SQLConnection.Execute.PartsTable.AddObject(p);
                SQLConnection.Execute.SaveDb();
                SetStatus();
                RefreshParts(); 
            }
        }

        private void PartModifiedActions(Parts p)
        {
            Parts modifiablePart = new Parts();

            modifiablePart = SQLConnection.Execute.PartsTable.Single(part => part.partID == p.partID);
            modifiablePart.partName = p.partName;
            modifiablePart.partManufacturer = p.partManufacturer;
            modifiablePart.partIDNumber = p.partIDNumber;
            modifiablePart.partPrice = p.partPrice;
            modifiablePart.partQuantity = p.partQuantity;
            modifiablePart.mustOrder = p.mustOrder;
            SQLConnection.Execute.SaveDb();
            SetStatus();
            RefreshParts();
        }

        private void SetStatus()
        {
            List<Parts> tempParts = new List<Parts>();
            ServiceWorksheets changableWS = new ServiceWorksheets();
            bool waitForPart = false;

            tempParts = SQLConnection.Execute.PartsTable.Where(p => p.serviceID == _serviceID).ToList();
            foreach (var item in tempParts)
            {
                if (item.mustOrder)
                {
                    waitForPart = true;
                    break;
                }
            }

            changableWS = SQLConnection.Execute.ServiceWorksheetsTable.Single(ws => ws.worksheetID == _serviceID);
            if (waitForPart)
            {
                if (selectedWorksheet.statusID != 2)
                {
	                changableWS.statusID = 4; 
                }
            }
            else
            {
                if (selectedWorksheet.quotRequested && !selectedWorksheet.quotAccepted)
                {
                    changableWS.statusID = 2;
                }
                else
                {
                    changableWS.statusID = 1;
                }
            }

            SQLConnection.Execute.SaveDb();
            RefreshGroupList();
        }

        private void CalcTotalCost()
        {
            totalCost = 0;
            foreach (var item in builtInParts)
            {
                totalCost += item.partPrice * (int)item.partQuantity;
            }
            if (_laborFee != null)
            {
                totalCost += (long)Math.Round((long)_laborFee * (1 - (discount / 100)));       
            }
        }

        private void FinishableCheck()
        {
            if (new List<long>(){2, 3, 4, 7}.Contains(selectedWorksheet.statusID))
            {
                finishable = false;
            }
            else
            {
                finishable = true;
            }
        }
    }    
}
