using MiddleLayer;
using MiddleLayer.Representations;
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
using Common.Enumerations;
using Common.Validations;

namespace GyorokRentService.ViewModel
{
    class NewCustomer_ViewModel : ViewModelBase
    {
        public event EventHandler CityRequested;
        public void OnCityRequested(EventArgs e)
        {
            if (CityRequested != null)
            {
                CityRequested(null, e);
            }
        }

        public event EventHandler CustomerInserted;
        public void OnCustomerInserted(EventArgs e)
        {
            if (CustomerInserted != null)
            {
                CustomerInserted(newCustomer, e);
            }
        }

        private CustomerBaseRepresentation _newCustomer;
        public CustomerBaseRepresentation newCustomer
        {
            get { return _newCustomer; }
            set
            {
                if (_newCustomer != value)
                {
                    _newCustomer = value;
                    RaisePropertyChanged("newCustomer");
                }
            }
        }
        
        private bool _isPerson;
        public bool isPerson
        {
            get { return _isPerson; }
            set
            {
                if (_isPerson != value)
                {
                    _isPerson = value;

                    if (value)
                        newCustomer = new PersonRepresentation() { ValidationRules = new PersonValidationForCreation() };
                    else
                        newCustomer = new FirmRepresentation() { ValidationRules = new FirmValidationForCreation() };

                    RaisePropertyChanged("isPerson");
                }
            }
        }

        searchCustomerTypeEnum _type;

        public ICommand insertNewCustomer { get { return new RelayCommand(insertNewCustomerExecute, () => true); } }
        void insertNewCustomerExecute()
        {
            newCustomer.id = DataProxy.Instance.AddCustomer(newCustomer);
            OnCustomerInserted(EventArgs.Empty);
            
        }
        public ICommand openCityChooser { get { return new RelayCommand(openCityChooserExecute, () => true); } }
        void openCityChooserExecute()
        {
            OnCityRequested(EventArgs.Empty);
        }

        public NewCustomer_ViewModel()
        {
            
        }
        public NewCustomer_ViewModel(searchCustomerTypeEnum displayType)
        {
            if (!this.IsInDesignMode)
            {
                isPerson = true;                    
                _type = displayType;
            }
        }
    }
}
