﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using GyorokRentService.ViewModel;
using SQLConnectionLib;
using System.Windows.Media;
using MiddleLayer.Representations;
using MiddleLayer;
using System.Threading;
using System.Threading.Tasks;
using Common.Enumerations;

namespace GyorokRentService.ViewModel
{
    public class searchCustomer_ModelView : ViewModelBase
    {
        public event EventHandler CustomerSelected;
        public void OnCustomerSelected(EventArgs e)
        {
            if (CustomerSelected != null)
            {
                CustomerSelected(_selectedCustomer, e);
            }
        }

        public event EventHandler NewCustomerRequested;
        public void OnNewCustomerRequested(EventArgs e)
        {
            if (NewCustomerRequested != null)
            {
                NewCustomerRequested(null, e);
            }
        }

        public List<CustomerBaseRepresentation> allCustomer;
        
        private CustomerBaseRepresentation _selectedCustomer;
        public CustomerBaseRepresentation selectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged("selectedCustomer");
                }
            }
        }

        private searchCustomerTypeEnum _searchCustomerType;
        public searchCustomerTypeEnum searchCustomerType
        {
            get { return _searchCustomerType; }
            set
            {
                if (_searchCustomerType != value)
                {
                    _searchCustomerType = value;
                    RaisePropertyChanged("searchCustomerType");
                }
            }
        }

        private FilterTypeEnum _filterType;
        public FilterTypeEnum filterType
        {
            get { return _filterType; }
            set
            {
                if (_filterType != value)
                {
                    _filterType = value;
                    RaisePropertyChanged("filterType");
                    FilterList();
                }
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        private string _searchText;
        private ObservableCollection<CustomerBaseRepresentation> _foundCustomers;
                
        public string searchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText == value)
                {
                    return;
                }

                _searchText = value;
                RaisePropertyChanged("searchText");
                FilterList();
            }
        }
        public ObservableCollection<CustomerBaseRepresentation> foundCustomers
        {
            get
            {
                return _foundCustomers;
            }

            set
            {
                if (_foundCustomers == value)
                {
                    return;
                }

                _foundCustomers = value;
                RaisePropertyChanged("foundCustomers");
            }
        }
        public ICommand customerSelected { get { return new RelayCommand(customerSelectedExecute, () => true); } }
        void customerSelectedExecute()
        {
            OnCustomerSelected(EventArgs.Empty);
        }
        public ICommand openNewCustomerWindow { get { return new RelayCommand(openNewCustomerWindowExecute, () => true); } }
        void openNewCustomerWindowExecute()
        {
            OnNewCustomerRequested(EventArgs.Empty);
        }
        public ICommand filterFirm { get { return new RelayCommand(filterFirmExecute, () => true); } }
        void filterFirmExecute()
        {
            filterType = FilterTypeEnum.Firm;
        }
        public ICommand filterPerson { get { return new RelayCommand(filterPersonExecute, () => true); } }
        void filterPersonExecute()
        {
            filterType = FilterTypeEnum.Person;
        }
        public ICommand filterBoth { get { return new RelayCommand(filterBothExecute, () => true); } }
        void filterBothExecute()
        {
            filterType = FilterTypeEnum.All;
        }
        public ICommand delPerson { get { return new RelayCommand(delPersonExecute, () => true); } }
        void delPersonExecute()
        {
            if (selectedCustomer != null)
            {
                MessageBoxResult result;

                result = MessageBox.Show("Biztosan törlöd?", "Partner törlése...", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    DataProxy.Instance.DeleteCustomerById(selectedCustomer.id);
                    allCustomer.Remove(selectedCustomer);
                    FilterList();
                    // TODO: Ha már ki volt választva, akkor a CustomerSelector-ból is törölni kell!
                }
            }
        }


        public searchCustomer_ModelView()
        {
            IsBusy = false;    
        }
        public searchCustomer_ModelView(searchCustomerTypeEnum controlType)
        {
            if (!this.IsInDesignMode)
            {
                _searchText = "";
                searchCustomerType = controlType;
                switch (controlType)
                {
                    case searchCustomerTypeEnum.Customer:
                        filterType = FilterTypeEnum.All;
                        break;
                    case searchCustomerTypeEnum.Contact:
                        filterType = FilterTypeEnum.Person;
                        break;
                    default:
                        break;
                }
                IsBusy = false;
                RefreshCustomerList();

                DataProxy.Instance.CustomersChanged += (s, a) =>
                {
                    RefreshCustomerList();
                };
                
            }
        }

        public void RefreshCustomerList()
        {
            Task t = new Task(() => 
            { 
                IsBusy = true;
                allCustomer = DataProxy.Instance.GetAllCustomers();
                FilterList();
                IsBusy = false;
            });
            t.Start();
        }

        private void FilterList()
        {
            if (allCustomer != null)
            {
                if (filterType == FilterTypeEnum.All)
                {
                    foundCustomers = new ObservableCollection<CustomerBaseRepresentation>(allCustomer.Where(c => c.customerName.ToLower().StartsWith(_searchText.ToLower())).OrderBy(oc => oc.customerName));
                }
                else
                {
                    foundCustomers = new ObservableCollection<CustomerBaseRepresentation>(allCustomer.Where(c => c.customerName.ToLower().StartsWith(_searchText.ToLower()) && c.isFirm == (filterType == FilterTypeEnum.Firm)).OrderBy(oc => oc.customerName));
                } 
            }
        }
    }
}
