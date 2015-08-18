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

namespace GyorokRentService.ViewModel
{
    class CustomerSelector_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;
        DateTime? dt;
        ObservableCollection<SQLConnectionLib.Customers> temp = new ObservableCollection<SQLConnectionLib.Customers>();
        private SQLConnectionLib.Customers _selectedCustomer;
        public SQLConnectionLib.Customers selectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {                
                _selectedCustomer = value;
                selectedCustomerName = value.customerName;
                selectedIdNumber = value.IDNumber;
                mothersName = value.mothersName;
                customerAddress = value.customerAddress;
                customerPhone = value.customerPhone;
                if (value.isFirm)
                {
                    isFirm = true;
                    birthDate = null;
                    refreshContacts();
                }
                else
                {
                    isPerson = true;                    
                    birthDate = value.birthDate;
                    if (contacts != null)
                    {
                        contacts.Clear(); 
                    }
                }
                workPlace = value.workPlace;
                comment = value.comment;
                problems = value.problems;
                defaultDiscount = (float)value.defaultDiscount * 100;
                isExpanded = false;
                
            }
        }
        public CustomerType customerMode;
        private long? _customerCityID;

        private string _selectedCustomerName;
        private string _selectedIdNumber;        
        private string _mothersName;
        private string _customerAddress;
        private string _customerPhone;
        private string _workPlace;
        private string _comment;
        private string _problems;
        private bool _isPerson;
        private bool _isFirm;
        private DateTime? _birthDate;
        private bool _isExpanded;
        private int _zExpander;
        private bool _readOnlyMode;
        private Visibility _modifyButtonVisibility;
        private Visibility _modifyEnableButtonVisibility;
        private ObservableCollection<SQLConnectionLib.Customers> _contacts;
        private SQLConnectionLib.Customers _selectedContact;
        private Visibility _commentSaveVisibility;
        private Visibility _wrongMothersName;
        private Visibility _wrongAddress;
        private Visibility _wrongPhone;
        private Visibility _wrongBirthDate;
        private Visibility _wrongWorkPlace;        
        private float _defaultDiscount;
        private bool _customerSelected;
        private string _customerNameLabel;
        private string _city;

        public string selectedCustomerName
        {
            get
            {
                return _selectedCustomerName;
            }

            set
            {
                if (_selectedCustomerName == value)
                {
                    return;
                }

                _selectedCustomerName = value;
                RaisePropertyChanged("selectedCustomerName");
            }
        }
        public string selectedIdNumber
        {
            get
            {
                return _selectedIdNumber;
            }

            set
            {
                if (_selectedIdNumber == value)
                {
                    return;
                }                
                _selectedIdNumber = value;
                RaisePropertyChanged("selectedIdNumber");
            }
        }
        public string mothersName
        {
            get
            {
                return _mothersName;
            }

            set
            {
                if (_mothersName == value && value != null && value != string.Empty)
                {
                    return;
                }

                _mothersName = value;
                RaisePropertyChanged("mothersName");
            }
        }
        public string customerAddress
        {
            get
            {
                return _customerAddress;
            }

            set
            {
                if (_customerAddress == value && value != null && value != string.Empty)
                {
                    return;
                }

                _customerAddress = value;
                RaisePropertyChanged("customerAddress");
            }
        }
        public string customerPhone
        {
            get
            {
                return _customerPhone;
            }

            set
            {
                if (_customerPhone == value && value != null && value != string.Empty)
                {
                    return;
                }

                _customerPhone = value;
                RaisePropertyChanged("customerPhone");
            }
        }
        public string workPlace
        {
            get
            {
                return _workPlace;
            }

            set
            {
                if (_workPlace == value && value != null && value != string.Empty)
                {
                    return;
                }

                _workPlace = value;
                RaisePropertyChanged("workPlace");
            }
        }
        public string comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (_comment == value)
                {
                    return;
                }

                _comment = value;
                RaisePropertyChanged("comment");
            }
        }
        public string problems
        {
            get
            {
                return _problems;
            }

            set
            {
                if (_problems == value)
                {
                    return;
                }

                _problems = value;
                RaisePropertyChanged("problems");
            }
        }
        public bool isPerson
        {
            get
            {
                return _isPerson;
            }

            set
            {
                if (_isPerson == value)
                {
                    return;
                }

                _isPerson = value;
                RaisePropertyChanged("isPerson");
                if (isPerson)
                {
                    isFirm = false;
                }
            }
        }
        public bool isFirm
        {
            get
            {
                return _isFirm;
            }

            set
            {
                if (_isFirm == value)
                {
                    return;
                }

                _isFirm = value;
                RaisePropertyChanged("isFirm");
                if (isFirm)
                {
                    isPerson = false;
                }
            }
        }
        public DateTime? birthDate
        {
            get
            {
                return _birthDate;
            }

            set
            {
                if (_birthDate == value && value != null)
                {
                    return;
                }

                _birthDate = value;
                RaisePropertyChanged("birthDate");
            }
        }
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
        public ObservableCollection<SQLConnectionLib.Customers> contacts
        {
            get
            {
                return _contacts;
            }

            set
            {
                if (_contacts == value)
                {
                    return;
                }

                _contacts = value;
                RaisePropertyChanged("contacts");
            }
        }
        public SQLConnectionLib.Customers selectedContact
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
        public float defaultDiscount
        {
            get
            {
                return _defaultDiscount;
            }

            set
            {
                if (_defaultDiscount == value)
                {
                    return;
                }

                _defaultDiscount = value;
                RaisePropertyChanged("defaultDiscount");
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
            Customers itemToModify = (from c in SQLConnection.Execute.CustomersTable where c.customerID == selectedCustomer.customerID select c).First();
            itemToModify.customerName = selectedCustomerName;
            itemToModify.IDNumber = selectedIdNumber;
            itemToModify.mothersName = mothersName;
            itemToModify.cityID = _customerCityID;
            itemToModify.customerAddress = customerAddress;
            itemToModify.customerPhone = customerPhone;
            itemToModify.workPlace = workPlace;
            itemToModify.comment = comment;
            itemToModify.problems = problems;
            if (itemToModify.isFirm)
            {
                itemToModify.birthDate = null;
            }
            else
            {
                itemToModify.birthDate = birthDate;
            }
            itemToModify.defaultDiscount = defaultDiscount / 100;
            SQLConnection.Execute.SaveDb();
            selectedCustomer = itemToModify;
            AppMessages.CustomerModified.Send(itemToModify);
            if (customerMode == CustomerType.Rent)
            {
                CheckRentValidation(itemToModify); 
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
                Contacts contact = SQLConnection.Execute.ContactsTable.First(c => c.firmID == selectedCustomer.customerID && c.agentID == selectedContact.customerID);
                SQLConnection.Execute.ContactsTable.DeleteObject(contact);
                SQLConnection.Execute.SaveDb();
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
            
            var modifiedCustomer = (from c in SQLConnection.Execute.CustomersTable where c.customerID == selectedCustomer.customerID select c).First();
            modifiedCustomer.comment = _comment;
            modifiedCustomer.problems = _problems;
            SQLConnection.Execute.SaveDb();

            commentSaveVisibility = Visibility.Hidden;
        }
        public ICommand openCityChooser { get { return new RelayCommand(openCityChooserExecute, () => true); } }
        void openCityChooserExecute()
        {
            View.SearchCity cityChooser = new View.SearchCity();
            cityChooser.ShowDialog();
        }

        public CustomerSelector_ViewModel()
        {
            
        }
        public CustomerSelector_ViewModel(CustomerType ct)
        {
            if (!this.IsInDesignMode)
            {
                isPerson = true;
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

                AppMessages.CustomerToSelect.Register(this, c =>
                {
                    Cities customerCity;

                    selectedCustomer = c;
                    if (selectedCustomer.cityID != null)
                    {
                        customerCity = SQLConnection.Execute.CitiesTable.Single<Cities>(ci => ci.cityID == selectedCustomer.cityID);
                        city = customerCity.postalCode + " " + customerCity.city;
                        _customerCityID = customerCity.cityID;
                    }
                    else
                    {
                        city = string.Empty;
                        _customerCityID = null;
                    }
                    customerSelected = true;
                    switch (customerMode)
                    {
                        case CustomerType.Rent:
                            CheckRentValidation(c);
                            break;
                        case CustomerType.Service:
                            break;
                        default:
                            break;
                    }
                });
                AppMessages.ContactPersonToSelect.Register(this, addContact);
                AppMessages.CityToSelect.Register(this, cty =>
                    {
                        if (!readOnlyMode)
                        {
                            _customerCityID = cty.cityID;
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

        private void addContact(SQLConnectionLib.Customers c)
        {
            //List<SQLConnectionLib.Contacts> contactList = new List<Contacts>();
            //contactList = SQLConnection.Execute.getContacts();
            if (!SQLConnection.Execute.ContactsTable.Any(contact => contact.firmID == selectedCustomer.customerID && contact.agentID == c.customerID))
            {
                Contacts cont = new Contacts();
                cont.firmID = selectedCustomer.customerID;
                cont.agentID = c.customerID;
                SQLConnection.Execute.ContactsTable.AddObject(cont);
                SQLConnection.Execute.SaveDb();
            }
            refreshContacts(); 
        }

        private void refreshContacts()
        {
            bool first = true;
            Customers firstCustomer = new Customers();
            temp.Clear();
            var qrycontacts = from agent in SQLConnection.Execute.CustomersTable
                              join firm in SQLConnection.Execute.ContactsTable on agent.customerID equals firm.agentID
                              where firm.firmID == selectedCustomer.customerID
                              select agent;
            foreach (Customers item in qrycontacts)
            {
                temp.Add(item);
                if (first)
                {
                    firstCustomer = item;
                    first = false;
                }
            }
            contacts = temp;
            selectedContact = firstCustomer;
        }

        private void CheckRentValidation(Customers c){
            bool valid = true;

            if (customerAddress == null || customerAddress == string.Empty)
            {
                wrongAddress = Visibility.Visible;
                valid = false;
            }
            else
            {
                wrongAddress = Visibility.Hidden;
            }

            if (customerPhone == null || customerPhone == string.Empty)
            {
                wrongPhone = Visibility.Visible;
                valid = false;
            }
            else
            {
                wrongPhone = Visibility.Hidden;
            }

            if (!isFirm)
            {
                if (mothersName == null || mothersName == string.Empty)
                {
                    wrongMothersName = Visibility.Visible;
                    valid = false;
                }
                else
                {
                    wrongMothersName = Visibility.Hidden;
                }

                if (birthDate == null)
                {
                    wrongBirthDate = Visibility.Visible;
                    valid = false;
                }
                else
                {
                    wrongBirthDate = Visibility.Hidden;
                }

                if (workPlace == null || workPlace == string.Empty)
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
    }

    public enum CustomerType { 
        Rent,
        Service
    }
}
