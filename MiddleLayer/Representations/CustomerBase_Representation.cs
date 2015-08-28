using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleLayer.Representations
{
    public class CustomerBase_Representation : RepresentationBase
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
                }
            }
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
                }
            }
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
                } }
        }

        private City_Representation _city;
        public City_Representation city
        {
            get { return _city; }
            set
            {
                if (_city != value)
                {
                    _city = value;
                    RaisePropertyChanged("city");
                } }
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
                } }
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
                } }
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
                } }
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
                } }
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
                } }
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
                } }
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
                } }
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
                } }
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
                } }
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
                } }
        }

        private ObservableCollection<CustomerBase_Representation> _contacts;
        public ObservableCollection<CustomerBase_Representation> contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts != value)
                {
                    _contacts = value;
                    RaisePropertyChanged("contacts");
                } }
        }
    }
}
