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
        
        private bool _isEditable;
        public bool isEditable
        {
            get { return _isEditable; }
            set
            {
                if (_isEditable != value)
                {
                    _isEditable = value;
                    RaisePropertyChanged("isEditable");
                }
            }
        }

        ObservableCollection<ToolRepresentation> temp = new ObservableCollection<ToolRepresentation>();
        private ToolRepresentation _selectedTool;
        public ToolRepresentation selectedTool
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
                isEditable = true;
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

            isEditable = false;
        }
        bool CandoModifyExecute()
        {
            return true;
        }

        public ToolSelector_ViewModel()
        {
            if (!this.IsInDesignMode)            
            {
                isEditable = false;
            }
        }
    }
}
