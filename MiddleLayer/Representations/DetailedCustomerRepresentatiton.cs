using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class DetailedCustomerRepresentatiton : RepresentationBase
    {
        private string _customerName;
        public string customerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    RaisePropertyChanged("customerName");
                } }
        }

        private string _IDNumber;
        public string IDNumber
        {
            get { return _IDNumber; }
            set
            {
                if (_IDNumber != value)
                {
                    _IDNumber = value;
                    RaisePropertyChanged("IDNumber");
                } }
        }
        
        private string _mothersName;
        public string mothersName
        {
            get { return _mothersName; }
            set
            {
                if (_mothersName != value)
                {
                    _mothersName = value;
                    RaisePropertyChanged("mothersName");
                }
            }
        }

        private string _customerAddress;
        public string customerAddress
        {
            get { return _customerAddress; }
            set
            {
                if (_customerAddress != value)
                {
                    _customerAddress = value;
                    RaisePropertyChanged("customerAddress");
                }
            }
        }

        private string _customerPhone;
        public string customerPhone
        {
            get { return _customerPhone; }
            set
            {
                if (_customerPhone != value)
                {
                    _customerPhone = value;
                    RaisePropertyChanged("customerPhone");
                }
            }
        }

        private DateTime? _birthDate;
        public DateTime? birthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    RaisePropertyChanged("birthDate");
                }
            }
        }

        private string _workplace;
        public string workplace
        {
            get { return _workplace; }
            set
            {
                if (_workplace != value)
                {
                    _workplace = value;
                    RaisePropertyChanged("workplace");
                }
            }
        }

        private bool _isFirm;
        public bool isFirm
        {
            get { return _isFirm; }
            set
            {
                if (_isFirm != value)
                {
                    _isFirm = value;
                    RaisePropertyChanged("isFirm");
                }
            }
        }

        private string _comment;
        public string comment
        {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    RaisePropertyChanged("comment");
                }
            }
        }

        private string _problems;
        public string problems
        {
            get { return _problems; }
            set
            {
                if (_problems != value)
                {
                    _problems = value;
                    RaisePropertyChanged("problems");
                }
            }
        }

        private double? _defaultDiscount;
        public double? defaultDiscount
        {
            get { return _defaultDiscount; }
            set
            {
                if (_defaultDiscount != value)
                {
                    _defaultDiscount = value;
                    RaisePropertyChanged("defaultDiscount");
                }
            }
        }

        private int _rentCounter;
        public int rentCounter
        {
            get { return _rentCounter; }
            set
            {
                if (_rentCounter != value)
                {
                    _rentCounter = value;
                    RaisePropertyChanged("rentCounter");
                }
            }
        }
        
        private int _serviceCounter;
        public int serviceCounter
        {
            get { return _serviceCounter; }
            set
            {
                if (_serviceCounter != value)
                {
                    _serviceCounter = value;
                    RaisePropertyChanged("serviceCounter");
                }
            }
        }

        private string _postalCode;
        public string postalCode
        {
            get { return _postalCode; }
            set
            {
                if (_postalCode != value)
                {
                    _postalCode = value;
                    RaisePropertyChanged("postalCode");
                }
            }
        }

        private string _city;
        public string city
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    RaisePropertyChanged("city");
                }
            }
        }

        private bool? _cityDeleted;
        public bool? cityDeleted
        {
            get { return _cityDeleted; }
            set
            {
                if (_cityDeleted != value)
                {
                    _cityDeleted = value;
                    RaisePropertyChanged("cityDeleted");
                }
            }
        }
    }
}
