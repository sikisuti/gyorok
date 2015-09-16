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
using GyorokRentService.View;

namespace GyorokRentService.ViewModel
{
    public class CustomerSelector_ViewModel : ViewModelBase
    {
        public event EventHandler customerPickerExpanded;
        public void OnCustomerPickerExpand()
        {
            if (customerPickerExpanded != null)
            {
                customerPickerExpanded(null, null);
            }
        }

        public event EventHandler customerPickerCollapsed;
        public void OnCustomerPickerCollapse()
        {
            if (customerPickerCollapsed != null)
            {
                customerPickerCollapsed(null, null);
            }
        }

        public event EventHandler cityRequest;
        public void OnCityRequest()
        {
            if (cityRequest != null)
            {
                cityRequest(null, EventArgs.Empty);
            }
        }

        public event EventHandler contactRequest;
        public void OnContactRequest()
        {
            if (contactRequest != null)
            {
                contactRequest(null, EventArgs.Empty);
            }
        }

        ObservableCollection<CustomerBase_Representation> temp = new ObservableCollection<CustomerBase_Representation>();
        private CustomerBase_Representation _selectedCustomer;
        public CustomerBase_Representation selectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if (_selectedCustomer != value)
                {
                    if (value != null) value.contacts = DataProxy.Instance.GetContacts(value);

                    _selectedCustomer = value;
                    RaisePropertyChanged("selectedCustomer");
                    RaisePropertyChanged("switchModifyMode");
                }              
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

        private bool _isReadonly;
        public bool isReadonly
        {
            get { return _isReadonly; }
            set
            {
                if (_isReadonly != value)
                {
                    _isReadonly = value;
                    RaisePropertyChanged("isReadonly");
                }
            }
        }

        public CustomerType customerMode;
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
                RaisePropertyChanged("deleteContact");

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
            OnCustomerPickerExpand();
            AppMessages.CustomerExpandChanged.Send(true);
        }
        bool CanonExpandExecute()
        {
            return true;
        }
        public ICommand onCollapse { get { return new RelayCommand(onCollapseExecute, CanonCollapseExecute); } }
        void onCollapseExecute()
        {
            OnCustomerPickerCollapse();
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
            return commentSaveVisibility == Visibility.Collapsed;
        }
        public ICommand switchModifyMode { get { return new RelayCommand(switchModifyModeExecute, CanswitchModifyModeExecute); } }
        void switchModifyModeExecute()
        {
            if (selectedCustomer != null)
            {
                isEditable = true;
                modifyEnableButtonVisibility = Visibility.Hidden; 
            }
        }
        bool CanswitchModifyModeExecute()
        {
            return selectedCustomer != null;
        }
        public ICommand doModify { get { return new RelayCommand(doModifyExecute, CandoModifyExecute); } }
        void doModifyExecute()
        {
            DataProxy.Instance.UpdateCustomer(selectedCustomer);
            AppMessages.CustomerModified.Send(selectedCustomer);
            if (customerMode == CustomerType.Rent)
            {
                //CheckRentValidation(selectedCustomer); 
            }
            
            isEditable = false;
            modifyEnableButtonVisibility = Visibility.Visible;
        }
        bool CandoModifyExecute()
        {
            return true;
        }
        public ICommand openEditContacts { get { return new RelayCommand(openEditContactsExecute, CanopenEditContactsExecute); } }
        void openEditContactsExecute()
        {
            OnContactRequest();
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
                selectedCustomer.contacts.Remove(selectedContact);
            }
            catch (Exception)
            {                
                return;
            }
            
        }
        bool CandeleteContactExecute()
        {
            return selectedContact != null;
        }
        public ICommand saveComments { get { return new RelayCommand(saveCommentsExecute, () => true); } }
        void saveCommentsExecute()
        {
            DataProxy.Instance.UpdateCustomer(selectedCustomer);

            commentSaveVisibility = Visibility.Collapsed;
        }
        public ICommand openCityChooser { get { return new RelayCommand(openCityChooserExecute, () => true); } }
        void openCityChooserExecute()
        {
            OnCityRequest();
        }

        public CustomerSelector_ViewModel()
        {            
        }
        public CustomerSelector_ViewModel(CustomerType ct)
        {
            if (!this.IsInDesignMode)
            {
                isEditable = false;
                isReadonly = false;
                commentSaveVisibility = Visibility.Collapsed;
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
            DataProxy.Instance.AddContact(selectedCustomer, c);
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
    }

    public enum CustomerType { 
        Rent,
        Service
    }
}
