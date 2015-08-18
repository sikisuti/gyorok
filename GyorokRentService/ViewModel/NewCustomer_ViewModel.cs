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

namespace GyorokRentService.ViewModel
{
    class NewCustomer_ViewModel : ViewModelBase
    {
        //dbGyorokEntities db;
        searchCustomerType _type;
        long _cityID;

        private string _newCustomerName;
        private string _newCustomerIdNumber;
        private string _newMothersName;
        private string _postalCode;
        private string _city;
        private string _newCustomerAddress;
        private string _newCustomerPhone;
        private string _newWorkPlace;
        private string _comment;
        private string _problems;
        private bool _isPerson;
        private bool _isFirm;
        private DateTime? _newBirthDate;
        private Visibility _visibleMode;
        private float _defaultDiscount;        

        public string newCustomerName
        {
            get
            {
                return _newCustomerName;
            }

            set
            {
                if (_newCustomerName == value)
                {
                    return;
                }

                _newCustomerName = value;
                RaisePropertyChanged("newCustomerName");
            }
        }
        public string newCustomerIdNumber
        {
            get
            {
                return _newCustomerIdNumber;
            }

            set
            {
                if (_newCustomerIdNumber == value)
                {
                    return;
                }
                _newCustomerIdNumber = value;
                RaisePropertyChanged("newCustomerIdNumber");
            }
        }
        public string newMothersName
        {
            get
            {
                return _newMothersName;
            }

            set
            {
                if (_newMothersName == value)
                {
                    return;
                }

                _newMothersName = value;
                RaisePropertyChanged("newMothersName");
            }
        }
        public string postalCode
        {
            get
            {
                return _postalCode;
            }

            set
            {
                if (_postalCode == value)
                {
                    return;
                }

                _postalCode = value;
                RaisePropertyChanged("postalCode");
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
        public string newCustomerAddress
        {
            get
            {
                return _newCustomerAddress;
            }

            set
            {
                if (_newCustomerAddress == value)
                {
                    return;
                }

                _newCustomerAddress = value;
                RaisePropertyChanged("newCustomerAddress");
            }
        }
        public string newCustomerPhone
        {
            get
            {
                return _newCustomerPhone;
            }

            set
            {
                if (_newCustomerPhone == value)
                {
                    return;
                }

                _newCustomerPhone = value;
                RaisePropertyChanged("newCustomerPhone");
            }
        }
        public string newWorkPlace
        {
            get
            {
                return _newWorkPlace;
            }

            set
            {
                if (_newWorkPlace == value)
                {
                    return;
                }

                _newWorkPlace = value;
                RaisePropertyChanged("newWorkPlace");
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
        public DateTime? newBirthDate
        {
            get
            {
                return _newBirthDate;
            }

            set
            {
                if (_newBirthDate == value)
                {
                    return;
                }

                _newBirthDate = value;
                RaisePropertyChanged("newBirthDate");
            }
        }
        public Visibility visibleMode
        {
            get
            {
                return _visibleMode;
            }

            set
            {
                if (_visibleMode == value)
                {
                    return;
                }

                _visibleMode = value;
                RaisePropertyChanged("visibleMode");
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

        public ICommand insertNewCustomer { get { return new RelayCommand(insertNewCustomerExecute, () => true); } }
        void insertNewCustomerExecute()
        {
            if (newCustomerName != null && newCustomerIdNumber != null)
            {
                SQLConnectionLib.Customers newCustomer = new SQLConnectionLib.Customers();
                newCustomer.customerName = newCustomerName;
                newCustomer.IDNumber = newCustomerIdNumber;
                newCustomer.mothersName = newMothersName;
                newCustomer.cityID = _cityID;
                newCustomer.customerAddress = newCustomerAddress;
                newCustomer.customerPhone = newCustomerPhone;
                newCustomer.workPlace = newWorkPlace;
                newCustomer.comment = comment;
                newCustomer.problems = problems;
                newCustomer.isFirm = isFirm;
                newCustomer.birthDate = newBirthDate;
                newCustomer.defaultDiscount = defaultDiscount / 100;
                newCustomer.rentCounter = 0;
                newCustomer.serviceCounter = 0;
                SQLConnection.Execute.CustomersTable.AddObject(newCustomer);
                SQLConnection.Execute.SaveDb();
                switch (_type)
                {
                    case searchCustomerType.searchCustomer:
                        AppMessages.CustomerToSelect.Send(newCustomer);
                        break;
                    case searchCustomerType.searchContact:
                        AppMessages.ContactPersonToSelect.Send(newCustomer);
                        break;
                    default:
                        break;
                }
                AppMessages.CloseNewCustomerWindow.Send("");
            }
            else
            {
                MessageBox.Show("Név és azonosító megadása kötelező!");
            }
        }
        public ICommand openCityChooser { get { return new RelayCommand(openCityChooserExecute, () => true); } }
        void openCityChooserExecute()
        {
            View.SearchCity cityChooser = new View.SearchCity();
            cityChooser.ShowDialog();
        }

        public NewCustomer_ViewModel()
        {
            
        }
        public NewCustomer_ViewModel(searchCustomerType displayType)
        {
            if (!this.IsInDesignMode)
            {
                isPerson = true;
                defaultDiscount = 0;
                newBirthDate = null;
                AppMessages.CityToSelect.Register(this, c => { _cityID = c.cityID; postalCode = c.postalCode; city = c.city; });
                switch (displayType)
                {
                    case searchCustomerType.searchCustomer:
                        _type = searchCustomerType.searchCustomer;
                        visibleMode = Visibility.Visible;
                        break;
                    case searchCustomerType.searchContact:
                        _type = searchCustomerType.searchContact;
                        visibleMode = Visibility.Hidden;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
