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

        private CustomerBase_Representation _newCustomer;
        public CustomerBase_Representation newCustomer
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
                newCustomer = new CustomerBase_Representation();
                newCustomer.isFirm = false;
                _type = displayType;
            }
        }
    }
}
