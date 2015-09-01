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
    class CustomerSelector_ViewModel : ViewModelBase
    {
        ObservableCollection<CustomerBase_Representation> temp = new ObservableCollection<CustomerBase_Representation>();
        private CustomerBase_Representation _selectedCustomer;
        public CustomerBase_Representation selectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged("selectedCustomer");
                }

                isExpanded = false;                
            }
        }
        public CustomerType customerMode;
                
        private bool _isExpanded;
        private int _zExpander;
        private bool _readOnlyMode;
        private Visibility _modifyButtonVisibility;
        private Visibility _modifyEnableButtonVisibility;
        private CustomerBase_Representation _selectedContact;
        private Visibility _commentSaveVisibility;
        private Visibility _wrongMothersName;
        private Visibility _wrongAddress;
        private Visibility _wrongPhone;
        private Visibility _wrongBirthDate;
        private Visibility _wrongWorkPlace; 
        private bool _customerSelected;
        private string _customerNameLabel;
        private string _city;
                   
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
        public CustomerBase_Representation selectedContact
        {
            get
            {
                return _selectedContact;
            }

            set
            {
                if (_selectedContact == value)
                {
                    return;
                }

                _selectedContact = value;
                RaisePropertyChanged("selectedContact");

                if (selectedContact != null)
                {
                    AppMessages.ContactSelected.Send(selectedContact);
                }
            }
        }
        public Visibility commentSaveVisibility
        {
            get
            {
                return _commentSaveVisibility;
            }

            set
            {
                if (_commentSaveVisibility == value)
                {
                    return;
                }

                _commentSaveVisibility = value;
                RaisePropertyChanged("commentSaveVisibility");
            }
        }
        public Visibility wrongMothersName
        {
            get
            {
                return _wrongMothersName;
            }

            set
            {
                if (_wrongMothersName == value)
                {
                    return;
                }

                _wrongMothersName = value;
                RaisePropertyChanged("wrongMothersName");
            }
        }
        public Visibility wrongAddress
        {
            get
            {
                return _wrongAddress;
            }

            set
            {
                if (_wrongAddress == value)
                {
                    return;
                }

                _wrongAddress = value;
                RaisePropertyChanged("wrongAddress");
            }
        }
        public Visibility wrongPhone
        {
            get
            {
                return _wrongPhone;
            }

            set
            {
                if (_wrongPhone == value)
                {
                    return;
                }

                _wrongPhone = value;
                RaisePropertyChanged("wrongPhone");
            }
        }
        public Visibility wrongBirthDate
        {
            get
            {
                return _wrongBirthDate;
            }

            set
            {
                if (_wrongBirthDate == value)
                {
                    return;
                }

                _wrongBirthDate = value;
                RaisePropertyChanged("wrongBirthDate");
            }
        }
        public Visibility wrongWorkPlace
        {
            get
            {
                return _wrongWorkPlace;
            }

            set
            {
                if (_wrongWorkPlace == value)
                {
                    return;
                }

                _wrongWorkPlace = value;
                RaisePropertyChanged("wrongWorkPlace");
            }
        }
        public bool customerSelected
        {
            get
            {
                return _customerSelected;
            }

            set
            {
                if (_customerSelected == value)
                {
                    return;
                }

                _customerSelected = value;
                RaisePropertyChanged("customerSelected");
            }
        }
        public string customerNameLabel
        {
            get
            {
                return _customerNameLabel;
            }

            set
            {
                if (_customerNameLabel == value)
                {
                    return;
                }

                _customerNameLabel = value;
                RaisePropertyChanged("customerNameLabel");
            }
        }
        public string city
        {
            get
            {
                return _city;
            }

            set
            {
                if (_city == value)
                {
                    return;
                }

                _city = value;
                RaisePropertyChanged("city");
            }
        }
        
        public ICommand onExpand { get { return new RelayCommand(onExpandExecute, CanonExpandExecute); } }
        void onExpandExecute()
        {
            zExpander = 2;
            AppMessages.CustomerExpandChanged.Send(true);
        }
        bool CanonExpandExecute()
        {
            return true;
        }
        public ICommand onCollapse { get { return new RelayCommand(onCollapseExecute, CanonCollapseExecute); } }
        void onCollapseExecute()
        {
            zExpander = 0;
            AppMessages.CustomerExpandChanged.Send(false);
        }
        bool CanonCollapseExecute()
        {
            return true;
        }
        public ICommand mustSave { get { return new RelayCommand(mustSaveExecute, CanmustSaveExecute); } }
        void mustSaveExecute()
        {
                commentSaveVisibility = Visibility.Visible;
        }
        bool CanmustSaveExecute()
        {
            return commentSaveVisibility == Visibility.Hidden;
        }
        public ICommand switchModifyMode { get { return new RelayCommand(switchModifyModeExecute, CanswitchModifyModeExecute); } }
        void switchModifyModeExecute()
        {
            if (selectedCustomer != null)
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
            DataProxy.Instance.UpdateCustomer(selectedCustomer);
            AppMessages.CustomerModified.Send(selectedCustomer);
            if (customerMode == CustomerType.Rent)
            {
                CheckRentValidation(selectedCustomer); 
            }

            readOnlyMode = true;
            modifyButtonVisibility = Visibility.Hidden;
            modifyEnableButtonVisibility = Visibility.Visible;
        }
        bool CandoModifyExecute()
        {
            return true;
        }
        public ICommand openEditContacts { get { return new RelayCommand(openEditContactsExecute, CanopenEditContactsExecute); } }
        void openEditContactsExecute()
        {
            View.SearchCustomerWindow ContactsWindow = new View.SearchCustomerWindow(searchCustomerType.searchContact);
            ContactsWindow.Show();
        }
        bool CanopenEditContactsExecute()
        {
            return true;
        }
        public ICommand deleteContact { get { return new RelayCommand(deleteContactExecute, CandeleteContactExecute); } }
        void deleteContactExecute()
        {
            try
            {
                DataProxy.Instance.DeleteContact(selectedCustomer, selectedContact);
                refreshContacts();
            }
            catch (Exception)
            {                
                return;
            }
            
        }
        bool CandeleteContactExecute()
        {
            return true;
        }
        public ICommand saveComments { get { return new RelayCommand(saveCommentsExecute, () => true); } }
        void saveCommentsExecute()
        {
            DataProxy.Instance.UpdateCustomer(selectedCustomer);

            commentSaveVisibility = Visibility.Hidden;
        }
        public ICommand openCityChooser { get { return new RelayCommand(openCityChooserExecute, () => true); } }
        void openCityChooserExecute()
        {
            View.SearchCity cityChooser = new View.SearchCity();
            ((SearchCity_ViewModel)cityChooser.DataContext).citySelected += (s, a) => { TODO };
            cityChooser.ShowDialog();
        }

        public CustomerSelector_ViewModel()
        {            
        }
        public CustomerSelector_ViewModel(CustomerType ct)
        {
            if (!this.IsInDesignMode)
            {
                commentSaveVisibility = Visibility.Hidden;
                readOnlyMode = true;
                modifyButtonVisibility = Visibility.Hidden;
                modifyEnableButtonVisibility = Visibility.Visible;

                customerMode = ct;

                switch (ct)
                {
                    case CustomerType.Rent:
                        customerNameLabel = "Kölcsönző neve:";
                        break;
                    case CustomerType.Service:
                        customerNameLabel = "Ügyfél neve:";
                        break;
                    default:
                        break;
                }
                AppMessages.ContactPersonToSelect.Register(this, addContact);
                AppMessages.CityToSelect.Register(this, cty =>
                    {
                        if (!readOnlyMode)
                        {
                            city = cty.postalCode + " " + cty.city;
                        }
                    });

                wrongMothersName = Visibility.Hidden;
                wrongAddress = Visibility.Hidden;
                wrongPhone = Visibility.Hidden;
                wrongBirthDate = Visibility.Hidden;
                wrongWorkPlace = Visibility.Hidden;
                customerSelected = false;
            }
        }

        private void addContact(CustomerBase_Representation c)
        {
            DataProxy.Instance.AddContact(selectedCustomer, selectedContact);
            refreshContacts(); 
        }

        private void refreshContacts()
        {
            selectedCustomer.contacts = DataProxy.Instance.GetContacts(selectedCustomer);
            if (selectedCustomer.contacts != null && selectedCustomer.contacts.Count() > 0)
            {
                selectedContact = selectedCustomer.contacts.FirstOrDefault();
            }            
        }

        private void CheckRentValidation(CustomerBase_Representation c){
            bool valid = true;

            if (selectedCustomer.customerAddress == null || selectedCustomer.customerAddress == string.Empty)
            {
                wrongAddress = Visibility.Visible;
                valid = false;
            }
            else
            {
                wrongAddress = Visibility.Hidden;
            }

            if (selectedCustomer.customerPhone == null || selectedCustomer.customerPhone == string.Empty)
            {
                wrongPhone = Visibility.Visible;
                valid = false;
            }
            else
            {
                wrongPhone = Visibility.Hidden;
            }

            if (!selectedCustomer.isFirm)
            {
                if (selectedCustomer.mothersName == null || selectedCustomer.mothersName == string.Empty)
                {
                    wrongMothersName = Visibility.Visible;
                    valid = false;
                }
                else
                {
                    wrongMothersName = Visibility.Hidden;
                }

                if (selectedCustomer.birthDate == null)
                {
                    wrongBirthDate = Visibility.Visible;
                    valid = false;
                }
                else
                {
                    wrongBirthDate = Visibility.Hidden;
                }

                if (selectedCustomer.workplace == null || selectedCustomer.workplace == string.Empty)
                {
                    wrongWorkPlace = Visibility.Visible;
                    valid = false;
                }
                else
                {
                    wrongWorkPlace = Visibility.Hidden;
                } 
            }

            if (valid) AppMessages.CustomerToRent.Send(c);
        }

        public void CustomerSelected(CustomerBase_Representation customer)
        {
            selectedCustomer = customer;
            customerSelected = true;
            switch (customerMode)
            {
                case CustomerType.Rent:
                    CheckRentValidation(customer);
                    break;
                case CustomerType.Service:
                    break;
                default:
                    break;
            }
        }
    }

    public enum CustomerType { 
        Rent,
        Service
    }
}
