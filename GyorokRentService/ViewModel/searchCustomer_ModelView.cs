using System;
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

namespace GyorokRentService.ViewModel
{
    class searchCustomer_ModelView : ViewModelBase
    {
        Customers _selectedCustomer; 
        searchCustomerType _custORcont;
        //CustomerType _rentORservice;
        int filterType;

        private string _searchText;
        private ObservableCollection<DetailedCustomers> _foundCustomers;
        private long _selectedCustomerID;
        private Visibility _visibilityType;
        private Brush _firmBorderBrush;
        public Brush firmBorderBrush
        {
            get
            {
                return _firmBorderBrush;
            }

            set
            {
                if (_firmBorderBrush == value)
                {
                    return;
                }

                _firmBorderBrush = value;
                RaisePropertyChanged("firmBorderBrush");
            }
        }

        private Brush _personBorderBrush;
        public Brush personBorderBrush
        {
            get
            {
                return _personBorderBrush;
            }

            set
            {
                if (_personBorderBrush == value)
                {
                    return;
                }

                _personBorderBrush = value;
                RaisePropertyChanged("personBorderBrush");
            }
        }

        private Brush _bothBorderBrush;
        public Brush bothBorderBrush
        {
            get
            {
                return _bothBorderBrush;
            }

            set
            {
                if (_bothBorderBrush == value)
                {
                    return;
                }

                _bothBorderBrush = value;
                RaisePropertyChanged("bothBorderBrush");
            }
        }
                
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
            }
        }
        public ObservableCollection<DetailedCustomers> foundCustomers
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
        public long selectedCustomerID
        {
            get
            {
                return _selectedCustomerID;
            }

            set
            {
                if (_selectedCustomerID == value)
                {
                    return;
                }

                _selectedCustomerID = value;
                RaisePropertyChanged("selectedCustomerID");
            }
        }
        public Visibility visibilityType
        {
            get
            {
                return _visibilityType;
            }

            set
            {
                if (_visibilityType == value)
                {
                    return;
                }

                _visibilityType = value;
                RaisePropertyChanged("visibilityType");
            }
        }

        public ICommand searchTextChanged { get { return new RelayCommand(searchTextChangedExecute, () => true); } }
        void searchTextChangedExecute()
        {
            RefreshCustomerList();
        }
        public ICommand customerSelected { get { return new RelayCommand(customerSelectedExecute, () => true); } }
        void customerSelectedExecute()
        {
            try
            {
                //db.Dispose();
                //db = new dbGyorokEntities();
                _selectedCustomer = (from c in SQLConnection.Execute.CustomersTable where c.customerID == _selectedCustomerID select c).First();
                switch (_custORcont)
                {
                    case searchCustomerType.searchCustomer:
                        AppMessages.CustomerToSelect.Send(_selectedCustomer);
                        break;
                    case searchCustomerType.searchContact:
                        AppMessages.ContactPersonToSelect.Send(_selectedCustomer);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                
            }
        }
        public ICommand openNewCustomerWindow { get { return new RelayCommand(openNewCustomerWindowExecute, () => true); } }
        void openNewCustomerWindowExecute()
        {
            View.NewCustomerWindow wndNewCustomer = new View.NewCustomerWindow(_custORcont);
            wndNewCustomer.Show();
        }
        public ICommand filterFirm { get { return new RelayCommand(filterFirmExecute, () => true); } }
        void filterFirmExecute()
        {
            if (filterType != 1)
            {
                filterType = 1;
                RefreshCustomerList();
                firmBorderBrush = null;
                personBorderBrush = Brushes.Black;
                bothBorderBrush = Brushes.Black;
            }
        }
        public ICommand filterPerson { get { return new RelayCommand(filterPersonExecute, () => true); } }
        void filterPersonExecute()
        {
            if (filterType != 2)
            {
                filterType = 2;
                RefreshCustomerList();
                firmBorderBrush = Brushes.Black;
                personBorderBrush = null;
                bothBorderBrush = Brushes.Black;
            }
        }
        public ICommand filterBoth { get { return new RelayCommand(filterBothExecute, () => true); } }
        void filterBothExecute()
        {
            if (filterType != 3)
            {
                filterType = 3;
                RefreshCustomerList();
                firmBorderBrush = Brushes.Black;
                personBorderBrush = Brushes.Black;
                bothBorderBrush = null;
            }
        }
        public ICommand delPerson { get { return new RelayCommand(delPersonExecute, () => true); } }
        void delPersonExecute()
        {
            if (_selectedCustomerID != 0)
            {
                MessageBoxResult result;

                result = MessageBox.Show("Biztosan törlöd?", "Partner törlése...", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    SQLConnection.Execute.delCustomer(_selectedCustomerID);
                    _selectedCustomerID = 0;
                    AppMessages.CustomerListModified.Send("");
                } 
            }
        }


        public searchCustomer_ModelView()
        {            
        }
        public searchCustomer_ModelView(searchCustomerType controlType)
        {
            if (!this.IsInDesignMode)
            {
                _searchText = "";
                AppMessages.CustomerListModified.Register(this, s => RefreshCustomerList());
                //AppMessages.ContactPersonToSelect.Register(this, (c) => RefreshCustomerList());
                //AppMessages.CustomerToSelect.Register(this, (c) => RefreshCustomerList()); 
                firmBorderBrush = Brushes.Black;
                personBorderBrush = Brushes.Black;
                switch (controlType)
                {
                    case searchCustomerType.searchCustomer: 
                        filterType = 3;
                        RefreshCustomerList();
                        _custORcont = searchCustomerType.searchCustomer;
                        visibilityType = Visibility.Visible;
                        AppMessages.CustomerModified.Register(this, s => RefreshCustomerList());
                        break;
                    case searchCustomerType.searchContact:
                        filterType = 2;
                        RefreshCustomerList();
                        _custORcont = searchCustomerType.searchContact;
                        visibilityType = Visibility.Hidden;
                        break;
                    default:
                        break;
                } 
            }
        }

        private void RefreshCustomerList()
        {
            if (filterType == 3)
            {
                foundCustomers = new ObservableCollection<DetailedCustomers>(SQLConnection.Execute.DetailedCustomersView.Where<DetailedCustomers>(c => c.customerName.StartsWith(_searchText) && c.customerDeleted == false).OrderBy<DetailedCustomers, string>(oc => oc.customerName).ToList()); 
            }
            else
            {
                foundCustomers = new ObservableCollection<DetailedCustomers>(SQLConnection.Execute.DetailedCustomersView.Where(c => c.customerName.StartsWith(_searchText) && c.isFirm == (filterType == 1) && c.customerDeleted == false).OrderBy<DetailedCustomers, string>(oc => oc.customerName).ToList());
            }
        }
    }
    public enum searchCustomerType
    {
        searchCustomer,
        searchContact
    }
}
