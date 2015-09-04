using Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class Rental_Representation : RepresentationBase
    {
        private CustomerBase_Representation _customer;
        public CustomerBase_Representation customer
        {
            get { return _customer; }
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    RaisePropertyChanged("customer");
                }
            }
        }

        private CustomerBase_Representation _contact;
        public CustomerBase_Representation contact
        {
            get { return _contact; }
            set
            {
                if (_contact != value)
                {
                    _contact = value;
                    RaisePropertyChanged("contact");
                }
            }
        }

        private Tool_Representation _tool;
        public Tool_Representation tool
        {
            get { return _tool; }
            set
            {
                if (_tool != value)
                {
                    _tool = value;
                    RaisePropertyChanged("tool");
                }
            }
        }

        private DateTime _rentalStart;
        public DateTime rentalStart
        {
            get { return _rentalStart; }
            set
            {
                if (_rentalStart != value)
                {
                    _rentalStart = value;
                    RaisePropertyChanged("rentalStart");
                }
            }
        }

        private DateTime _rentalEnd;
        public DateTime rentalEnd
        {
            get { return _rentalEnd; }
            set
            {
                if (_rentalEnd != value)
                {
                    _rentalEnd = value;
                    RaisePropertyChanged("rentalEnd");
                }
            }
        }

        private DateTime? _rentalRealEnd;
        public DateTime? rentalRealEnd
        {
            get { return _rentalRealEnd; }
            set
            {
                if (_rentalRealEnd != value)
                {
                    _rentalRealEnd = value;
                    RaisePropertyChanged("rentalRealEnd");
                }
            }
        }

        private long _actualPrice;
        public long actualPrice
        {
            get { return _actualPrice; }
            set
            {
                if (_actualPrice != value)
                {
                    _actualPrice = value;
                    RaisePropertyChanged("actualPrice");
                }
            }
        }

        private PayType_Representation _payType;
        public PayType_Representation payType
        {
            get { return _payType; }
            set
            {
                if (_payType != value)
                {
                    _payType = value;
                    RaisePropertyChanged("payType");
                }
            }
        }

        private float _discount;
        public float discount
        {
            get { return _discount; }
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    RaisePropertyChanged("discount");
                }
            }
        }

        private bool _isClean;
        public bool isClean
        {
            get { return _isClean; }
            set
            {
                if (_isClean != value)
                {
                    _isClean = value;
                    RaisePropertyChanged("isClean");
                }
            }
        }

        private RentalGroup_Representation _group;
        public RentalGroup_Representation group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    RaisePropertyChanged("group");
                }
            }
        }

        private bool _isPaid;
        public bool isPaid
        {
            get { return _isPaid; }
            set
            {
                if (_isPaid != value)
                {
                    _isPaid = value;
                    RaisePropertyChanged("isPaid");
                }
            }
        }
    }
}
