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
                    RaisePropertyChanged("ActualPrice");
                    if (group != null)
                    {
                        group.AnyRentalChangeAction();
                        DataProxy.Instance.UpdateRental(this);
                    }
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

        public int PlannedHours {
            get
            {
                switch (rentTerm)
                {
                    case RentTermEnum.OneHour:
                        return 1;
                        break;
                    case RentTermEnum.HalfDay:
                        return (int)Math.Round((double)(DataProxy.Instance.HoursPerDay / 2), 0);
                        break;
                    case RentTermEnum.OneDay:
                        return DataProxy.Instance.HoursPerDay;
                        break;
                    case RentTermEnum.ThreeDays:
                        return DataProxy.Instance.HoursPerDay * 3;
                        break;
                    case RentTermEnum.OneWeek:
                        return DataProxy.Instance.HoursPerDay * 7;
                        break;
                    case RentTermEnum.Custom:
                        return (rentalEnd - rentalStart).Days;
                        break;
                    default:
                        return 0;
                        break;
                }
            }
        }

        public double ElapsedDays { get { return ((rentalRealEnd ?? DateTime.Now) - rentalStart).Days; } }

        public double ElapsedHours
        {
            get
            {
                var hours = ((rentalRealEnd ?? DateTime.Now) - rentalStart).Hours + 1;
                if (hours > DataProxy.Instance.HoursPerDay) return DataProxy.Instance.HoursPerDay;
                return hours;
            }
        }

        public long PlannedPrice { get { return (long)Math.Round(PlannedHours * (tool.rentPrice / DataProxy.Instance.HoursPerDay) * (1 - discount), 0); } }

        public long ActualPrice { get { return (long)Math.Round(((ElapsedDays * DataProxy.Instance.HoursPerDay) + ElapsedHours) * (tool.rentPrice / DataProxy.Instance.HoursPerDay) * (1 - discount), 0) + (isClean ? 0 : 100); } }

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
