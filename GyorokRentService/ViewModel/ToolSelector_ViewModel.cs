using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Globalization;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GyorokRentService.ViewModel;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class ToolSelector_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;
        ObservableCollection<Tools> temp = new ObservableCollection<Tools>();
        private Tools _selectedTool;
        public Tools selectedTool
        {
            get { return _selectedTool; }
            set
            {
                if (value != null)
                {
                    _selectedTool = value;
                    selectedToolName = value.toolName;
                    selectedManufacturer = value.toolManufacturer;
                    selectedIdNumber = value.IDNumber;
                    selectedSerial = value.serialNumber;
                    selectedRentPrice = value.rentPrice.ToString("C0");
                    defaultDeposit = (long)value.defaultDeposit;
                    isExpanded = false;   
                }
                else
                {
                    _selectedTool = value;
                    selectedToolName = string.Empty;
                    selectedManufacturer = string.Empty;
                    selectedIdNumber = string.Empty;
                    selectedSerial = string.Empty;
                    selectedRentPrice = string.Empty;
                    defaultDeposit = null;
                    isExpanded = false;   
                }
            }
        }

        private string _selectedToolName;       
        private string _selectedManufacturer;
        private string _selectedIdNumber; 
        private string _selectedSerial;
        private string _selectedRentPrice;        
        private bool _isExpanded;
        private int _zExpander;
        private bool _readOnlyMode;
        private Visibility _modifyButtonVisibility;
        private Visibility _modifyEnableButtonVisibility;
        private long? _defaultDeposit;        

        public string selectedToolName
        {
            get
            {
                return _selectedToolName;
            }

            set
            {
                if (_selectedToolName == value)
                {
                    return;
                }

                _selectedToolName = value;
                RaisePropertyChanged("selectedToolName");
            }
        }
        public string selectedManufacturer
        {
            get
            {
                return _selectedManufacturer;
            }

            set
            {
                if (_selectedManufacturer == value)
                {
                    return;
                }

                _selectedManufacturer = value;
                RaisePropertyChanged("selectedManufacturer");
            }
        }
        public string selectedIdNumber
        {
            get
            {
                return _selectedIdNumber;
            }

            set
            {
                if (_selectedIdNumber == value)
                {
                    return;
                }                
                _selectedIdNumber = value;
                RaisePropertyChanged("selectedIdNumber");
            }
        }
        public string selectedSerial
        {
            get
            {
                return _selectedSerial;
            }

            set
            {
                if (_selectedSerial == value)
                {
                    return;
                }

                _selectedSerial = value;
                RaisePropertyChanged("selectedSerial");
            }
        }
        public string selectedRentPrice
        {
            get
            {
                return _selectedRentPrice;
            }

            set
            {
                if (_selectedRentPrice == value)
                {
                    return;
                }

                _selectedRentPrice = value;
                RaisePropertyChanged("selectedRentPrice");
            }
        }        
        public bool isExpanded
        {
            get
            {
                return _isExpanded;
            }

            set
            {
                if (_isExpanded == value)
                {
                    return;
                }

                _isExpanded = value;
                RaisePropertyChanged("isExpanded");
            }
        }
        public int zExpander
        {
            get
            {
                return _zExpander;
            }

            set
            {
                if (_zExpander == value)
                {
                    return;
                }

                _zExpander = value;
                RaisePropertyChanged("zExpander");
            }
        }
        public bool readOnlyMode
        {
            get
            {
                return _readOnlyMode;
            }

            set
            {
                if (_readOnlyMode == value)
                {
                    return;
                }
                if (selectedRentPrice != null)
                {
                    if (value)
                    {
                        selectedRentPrice = selectedTool.rentPrice.ToString("C0");
                    }
                    else
                    {
                        selectedRentPrice = selectedTool.rentPrice.ToString();
                    }
                }
                

                _readOnlyMode = value;
                RaisePropertyChanged("readOnlyMode");
            }
        }
        public Visibility modifyButtonVisibility
        {
            get
            {
                return _modifyButtonVisibility;
            }

            set
            {
                if (_modifyButtonVisibility == value)
                {
                    return;
                }

                _modifyButtonVisibility = value;
                RaisePropertyChanged("modifyButtonVisibility");
            }
        }
        public Visibility modifyEnableButtonVisibility
        {
            get
            {
                return _modifyEnableButtonVisibility;
            }

            set
            {
                if (_modifyEnableButtonVisibility == value)
                {
                    return;
                }

                _modifyEnableButtonVisibility = value;
                RaisePropertyChanged("modifyEnableButtonVisibility");
            }
        }
        public long? defaultDeposit
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
        
        public ICommand onExpand { get { return new RelayCommand(onExpandExecute, CanonExpandExecute); } }
        void onExpandExecute()
        {
            zExpander = 2;
            AppMessages.ToolExpandChanged.Send(true);
        }
        bool CanonExpandExecute()
        {
            return true;
        }
        public ICommand onCollapse { get { return new RelayCommand(onCollapseExecute, CanonCollapseExecute); } }
        void onCollapseExecute()
        {
            zExpander = 0;
            AppMessages.ToolExpandChanged.Send(false);
        }
        bool CanonCollapseExecute()
        {
            return true;
        }
        public ICommand switchModifyMode { get { return new RelayCommand(switchModifyModeExecute, CanswitchModifyModeExecute); } }
        void switchModifyModeExecute()
        {
            if (selectedTool != null)
            {
                readOnlyMode = false;
                modifyButtonVisibility = Visibility.Visible;
                modifyEnableButtonVisibility = Visibility.Hidden; 
            }
        }
        bool CanswitchModifyModeExecute()
        {
            return true;
        }
        public ICommand doModify { get { return new RelayCommand(doModifyExecute, CandoModifyExecute); } }
        void doModifyExecute()
        {
            long l;
            
            Tools itemToModify = (from t in SQLConnection.Execute.ToolsTable where t.toolID == selectedTool.toolID select t).First();
            itemToModify.toolName = selectedToolName;
            itemToModify.toolManufacturer = selectedManufacturer;
            itemToModify.IDNumber = selectedIdNumber;
            itemToModify.serialNumber = selectedSerial;
            long.TryParse(selectedRentPrice, out l);
            itemToModify.rentPrice = l;
            itemToModify.defaultDeposit = defaultDeposit;
            SQLConnection.Execute.SaveDb();
            selectedTool = itemToModify;
            AppMessages.ToolModified.Send(itemToModify);

            readOnlyMode = true;
            modifyButtonVisibility = Visibility.Hidden;
            modifyEnableButtonVisibility = Visibility.Visible; 
        }
        bool CandoModifyExecute()
        {
            return true;
        }

        public ToolSelector_ViewModel()
        {
            if (!this.IsInDesignMode)            
            {
                //db = new dbGyorokEntities();
                readOnlyMode = true;
                modifyButtonVisibility = Visibility.Hidden;
                modifyEnableButtonVisibility = Visibility.Visible;

                AppMessages.ToolToSelect.Register(this, setTool);
                AppMessages.NewRentAdded.Register(this, r => { selectedTool = null; });
            }
        }
        private void setTool(Tools t)
        {
            selectedTool = t;
        }
    }
}
