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
using MiddleLayer.Representations;
using MiddleLayer;
using System.Threading;
using System.Threading.Tasks;

namespace GyorokRentService.ViewModel
{
    public class searchCustomer_ModelView : ViewModelBase
    {
        public event EventHandler CustomerSelected;
        public void OnCustomerSelected(EventArgs e)
        {
            if (customerSelected != null)
            {
                CustomerSelected(_selectedCustomer, e);
            }
        }

        public List<CustomerBase_Representation> allCustomer;

        CustomerBase_Representation _selectedCustomer; 
        searchCustomerType _custORcont;
        //CustomerType _rentORservice;
        int filterType;

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
        private ObservableCollection<CustomerBase_Representation> _foundCustomers;
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
        public ObservableCollection<CustomerBase_Representation> foundCustomers
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
            FilterList();
        }
        public ICommand customerSelected { get { return new RelayCommand(customerSelectedExecute, () => true); } }
        void customerSelectedExecute()
        {
            try
            {
                //db.Dispose();
                //db = new dbGyorokEntities();
                _selectedCustomer = DataProxy.Instance.GetCustomerById(_selectedCustomerID);
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
                OnCustomerSelected(null);
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
                FilterList();
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
                FilterList();
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
                FilterList();
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
            IsBusy = false;    
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
                        _custORcont = searchCustomerType.searchCustomer;
                        visibilityType = Visibility.Visible;
                        AppMessages.CustomerModified.Register(this, s => RefreshCustomerList());
                        break;
                    case searchCustomerType.searchContact:
                        filterType = 2;
                        _custORcont = searchCustomerType.searchContact;
                        visibilityType = Visibility.Hidden;
                        break;
                    default:
                        break;
                }
                IsBusy = false;
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
                if (filterType == 3)
                {
                    foundCustomers = new ObservableCollection<CustomerBase_Representation>(allCustomer.Where(c => c.customerName.ToLower().StartsWith(_searchText.ToLower())).OrderBy(oc => oc.customerName));
                }
                else
                {
                    foundCustomers = new ObservableCollection<CustomerBase_Representation>(allCustomer.Where(c => c.customerName.ToLower().StartsWith(_searchText.ToLower()) && c.isFirm == (filterType == 1)).OrderBy(oc => oc.customerName));
                } 
            }
        }
    }
    public enum searchCustomerType
    {
        searchCustomer,
        searchContact
    }
}
