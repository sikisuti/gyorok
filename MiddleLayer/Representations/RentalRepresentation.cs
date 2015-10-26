using Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class RentalRepresentation : RepresentationBase, ICloneable
    {
        private CustomerBaseRepresentation _customer;
        public CustomerBaseRepresentation customer
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

        private CustomerBaseRepresentation _contact;
        public CustomerBaseRepresentation contact
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

        private ToolRepresentation _tool;
        public ToolRepresentation tool
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

        private PayTypeRepresentation _payType;
        public PayTypeRepresentation payType
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

        private double _discount;
        public double discount
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

        private RentTermEnum _rentTerm;
        public RentTermEnum rentTerm
        {
            get { return _rentTerm; }
            set
            {
                if (_rentTerm != value)
                {
                    if (value != RentTermEnum.Custom)
                    {
                        rentalEnd = new DateTime(0);
                    }
                    else
                    {
                        rentalEnd = DateTime.Now.AddDays(1);
                    }
                    _rentTerm = value;
                    RaisePropertyChanged("rentTerm");
                }
            }
        }

        public decimal IntervalDay {
            get
            {
                decimal rtn = 0;

                switch (rentTerm)
                {
                    case RentTermEnum.OneHour:
                        return 0.2m;
                        break;
                    case RentTermEnum.HalfDay:
                        return 0.5m;
                        break;
                    case RentTermEnum.OneDay:
                        return 1;
                        break;
                    case RentTermEnum.ThreeDays:
                        rtn = 3;
                        break;
                    case RentTermEnum.OneWeek:
                        rtn = 7;
                        break;
                    case RentTermEnum.Custom:
                        rtn = (rentalEnd - rentalStart).Days;
                        break;
                    default:
                        break;
                }
                return rtn;
            }
        }

        public long TotalPrice { get { return (long)Math.Round(IntervalDay * tool.rentPrice, 0); } }

        public RentalRepresentation()
        {
            rentTerm = RentTermEnum.OneDay;
        }

        public object Clone()
        {
            RentalRepresentation clonedRental = this.MemberwiseClone() as RentalRepresentation;

            return clonedRental;
        }
    }
}
