using Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class WorksheetRepresentation : RepresentationBase
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

        private List<PartRepresentation> _parts;
        public List<PartRepresentation> parts
        {
            get { return _parts; }
            set
            {
                if (_parts != value)
                {
                    _parts = value;
                    RaisePropertyChanged("parts");
                }
            }
        }

        private string _serialID;
        public string serialID
        {
            get { return _serialID; }
            set
            {
                if (_serialID != value)
                {
                    _serialID = value;
                    RaisePropertyChanged("serialID");
                }
            }
        }

        private string _deviceID;
        public string deviceID
        {
            get { return _deviceID; }
            set
            {
                if (_deviceID != value)
                {
                    _deviceID = value;
                    RaisePropertyChanged("deviceID");
                }
            }
        }

        private string _deviceManufacturer;
        public string deviceManufacturer
        {
            get { return _deviceManufacturer; }
            set
            {
                if (_deviceManufacturer != value)
                {
                    _deviceManufacturer = value;
                    RaisePropertyChanged("deviceManufacturer");
                }
            }
        }

        private string _deviceName;
        public string deviceName
        {
            get { return _deviceName; }
            set
            {
                if (_deviceName != value)
                {
                    _deviceName = value;
                    RaisePropertyChanged("deviceName");
                }
            }
        }

        private bool _hasWarranty;
        public bool hasWarranty
        {
            get { return _hasWarranty; }
            set
            {
                if (_hasWarranty != value)
                {
                    _hasWarranty = value;
                    RaisePropertyChanged("hasWarranty");
                }
            }
        }

        private ErrorTypeEnum _errorType;
        public ErrorTypeEnum errorType
        {
            get { return _errorType; }
            set
            {
                if (_errorType != value)
                {
                    _errorType = value;
                    RaisePropertyChanged("errorType");
                }
            }
        }

        private string _errorDescription;
        public string errorDescription
        {
            get { return _errorDescription; }
            set
            {
                if (_errorDescription != value)
                {
                    _errorDescription = value;
                    RaisePropertyChanged("errorDescription");
                }
            }
        }

        private ServiceStatusEnum _status;
        public ServiceStatusEnum status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged("status");
                }
            }
        }

        private DateTime _serviceStart;
        public DateTime serviceStart
        {
            get { return _serviceStart; }
            set
            {
                if (_serviceStart != value)
                {
                    _serviceStart = value;
                    RaisePropertyChanged("serviceStart");
                }
            }
        }

        private DateTime? _serviceEnd;
        public DateTime? serviceEnd
        {
            get { return _serviceEnd; }
            set
            {
                if (_serviceEnd != value)
                {
                    _serviceEnd = value;
                    RaisePropertyChanged("serviceEnd");
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

        private ServiceGroupRepresentation _serviceGroup;
        public ServiceGroupRepresentation serviceGroup
        {
            get { return _serviceGroup; }
            set
            {
                if (_serviceGroup != value)
                {
                    _serviceGroup = value;
                    RaisePropertyChanged("serviceGroup");
                }
            }
        }

        private long? _serviceCost;
        public long? serviceCost
        {
            get { return _serviceCost; }
            set
            {
                if (_serviceCost != value)
                {
                    _serviceCost = value;
                    RaisePropertyChanged("serviceCost");
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

        private bool _inQuotMode;
        public bool inQuotMode
        {
            get { return _inQuotMode; }
            set
            {
                if (_inQuotMode != value)
                {
                    _inQuotMode = value;
                    RaisePropertyChanged("inQuotMode");
                }
            }
        }

        private bool _quotRequested;
        public bool quotRequested
        {
            get { return _quotRequested; }
            set
            {
                if (_quotRequested != value)
                {
                    _quotRequested = value;
                    RaisePropertyChanged("quotRequested");
                }
            }
        }

        private bool _quotAccepted;
        public bool quotAccepted
        {
            get { return _quotAccepted; }
            set
            {
                if (_quotAccepted != value)
                {
                    _quotAccepted = value;
                    RaisePropertyChanged("quotAccepted");
                }
            }
        }

        private string _plusParts;
        public string plusParts
        {
            get { return _plusParts; }
            set
            {
                if (_plusParts != value)
                {
                    _plusParts = value;
                    RaisePropertyChanged("plusParts");
                }
            }
        }

        private int _yearCounter;
        public int yearCounter
        {
            get { return _yearCounter; }
            set
            {
                if (_yearCounter != value)
                {
                    _yearCounter = value;
                    RaisePropertyChanged("yearCounter");
                }
            }
        }

        private DateTime? _buyDate;
        public DateTime? buyDate
        {
            get { return _buyDate; }
            set
            {
                if (_buyDate != value)
                {
                    _buyDate = value;
                    RaisePropertyChanged("buyDate");
                }
            }
        }

        private string _warrantyNumber;
        public string warrantyNumber
        {
            get { return _warrantyNumber; }
            set
            {
                if (_warrantyNumber != value)
                {
                    _warrantyNumber = value;
                    RaisePropertyChanged("warrantyNumber");
                }
            }
        }

        private long? _maxCost;
        public long? maxCost
        {
            get { return _maxCost; }
            set
            {
                if (_maxCost != value)
                {
                    _maxCost = value;
                    RaisePropertyChanged("maxCost");
                }
            }
        }

        public WorksheetRepresentation()
        {
            errorType = ErrorTypeEnum.Electrical;
        }
    }
}
