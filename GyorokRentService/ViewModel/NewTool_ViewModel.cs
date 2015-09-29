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
using MiddleLayer.Representations;
using MiddleLayer;
using Common.Validations;

namespace GyorokRentService.ViewModel
{
    class NewTool_ViewModel : ViewModelBase
    {
        public event EventHandler ToolInserted;
        private void OnToolInserted(EventArgs e)
        {
            if (ToolInserted != null)
            {
                ToolInserted(newTool, e);
            }
        }

        private Tool_Representation _newTool;
        public Tool_Representation newTool
        {
            get { return _newTool; }
            set
            {
                if (_newTool != value)
                {
                    _newTool = value;
                    RaisePropertyChanged("newTool");
                }
            }
        }
        
        private bool _contentOK = false;               
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
            newTool.id = DataProxy.Instance.AddTool(newTool);
            OnToolInserted(EventArgs.Empty);
        }

        public NewTool_ViewModel()
        {
            if (!this.IsInDesignMode)
            {
                newTool = new Tool_Representation() { ValidationRules = new ToolValidationRules() };
                newTool.toolStatus = new ToolStatus_Representation() { id = 1 };
                newTool.fromDate = DateTime.Today;
                newTool.defaultDeposit = 0;
            }
        }
    }
}
