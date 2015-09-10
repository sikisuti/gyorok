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
using MiddleLayer.Representations;
using MiddleLayer;

namespace GyorokRentService.ViewModel
{
    public class ToolSelector_ViewModel : ViewModelBase
    {
        public event EventHandler ToolPickerExpanded;
        public void OnToolPickerExpanded()
        {
            if (ToolPickerExpanded != null)
            {
                ToolPickerExpanded(null, null);
            }
        }

        ObservableCollection<Tool_Representation> temp = new ObservableCollection<Tool_Representation>();
        private Tool_Representation _selectedTool;
        public Tool_Representation selectedTool
        {
            get { return _selectedTool; }
            set
            {
                if (value != _selectedTool)
                {
                    _selectedTool = value;
                    isExpanded = false;
                    RaisePropertyChanged("selectedTool");
                }
            }
        }
       
        private bool _isExpanded;
        private int _zExpander;
        private bool _readOnlyMode;
        private Visibility _modifyButtonVisibility;
        private Visibility _modifyEnableButtonVisibility;
        
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
        
        public ICommand onExpand { get { return new RelayCommand(onExpandExecute, () => true); } }
        void onExpandExecute()
        {
            OnToolPickerExpanded();
            zExpander = 2;
            //AppMessages.ToolExpandChanged.Send(true);
        }
        public ICommand onCollapse { get { return new RelayCommand(onCollapseExecute, () => true); } }
        void onCollapseExecute()
        {
            zExpander = 0;
            AppMessages.ToolExpandChanged.Send(false);
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
            DataProxy.Instance.UpdateTool(selectedTool);
            AppMessages.ToolModified.Send(selectedTool);

            readOnlyMode = true;
            modifyButtonVisibility = Visibility.Collapsed;
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
                readOnlyMode = true;
                modifyButtonVisibility = Visibility.Collapsed;
                modifyEnableButtonVisibility = Visibility.Visible;

                //AppMessages.ToolToSelect.Register(this, setTool);
                AppMessages.NewRentAdded.Register(this, r => { selectedTool = null; });
            }
        }
    }
}
