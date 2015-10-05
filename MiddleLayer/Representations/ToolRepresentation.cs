using Common.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class ToolRepresentation : RepresentationBase, IDataErrorInfo
    {
        private string _toolName;
        public string toolName
        {
            get { return _toolName; }
            set
            {
                if (_toolName != value)
                {
                    _toolName = value;
                    RaisePropertyChanged("toolName");
                }
            }
        }

        private string _toolManufacturer;
        public string toolManufacturer
        {
            get { return _toolManufacturer; }
            set
            {
                if (_toolManufacturer != value)
                {
                    _toolManufacturer = value;
                    RaisePropertyChanged("toolManufacturer");
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

        private string _serialNumber;
        public string serialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (_serialNumber != value)
                {
                    _serialNumber = value;
                    RaisePropertyChanged("serialNumber");
                }
            }
        }

        private long _rentPrice;
        public long rentPrice
        {
            get { return _rentPrice; }
            set
            {
                if (_rentPrice != value)
                {
                    _rentPrice = value;
                    RaisePropertyChanged("rentPrice");
                }
            }
        }

        private DateTime _fromDate;
        public DateTime fromDate
        {
            get { return _fromDate; }
            set
            {
                if (_fromDate != value)
                {
                    _fromDate = value;
                    RaisePropertyChanged("fromDate");
                }
            }
        }

        private long? _defaultDeposit;
        public long? defaultDeposit
        {
            get { return _defaultDeposit; }
            set
            {
                if (_defaultDeposit != value)
                {
                    _defaultDeposit = value;
                    RaisePropertyChanged("defaultDeposit");
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
        
        private ToolStatusRepresentation _toolStatus;
        public ToolStatusRepresentation toolStatus
        {
            get { return _toolStatus; }
            set
            {
                if (_toolStatus != value)
                {
                    _toolStatus = value;
                    RaisePropertyChanged("toolStatus");
                }
            }
        }

        public IToolValidationRules ValidationRules { get; set; }

        public bool IsValid
        {
            get{ return string.IsNullOrEmpty(Error); }            
        }

        public string Error
        {
            get
            {
                if (ValidationRules == null)
                    return "Nincs gép érvényességi szabály";

                string errorMessage = string.Empty;

                if (this["toolName"] != string.Empty)
                {
                    if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                    errorMessage += "Gép név nem megfelelő";
                }
                if (this["toolManufacturer"] != string.Empty)
                {
                    if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                    errorMessage += "Gép gyártó nem megfelelő";
                }
                if (this["IDNumber"] != string.Empty)
                {
                    if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                    errorMessage += "Gép azonosító nem megfelelő";
                }
                if (this["serialNumber"] != string.Empty)
                {
                    if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                    errorMessage += "Gép gyári azonosító nem megfelelő";
                }
                if (this["rentPrice"] != string.Empty)
                {
                    if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                    errorMessage += "Gép bérlési költség nem megfelelő";
                }
                if (this["fromDate"] != string.Empty)
                {
                    if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                    errorMessage += "Gép beiktatási dátum nem megfelelő";
                }
                if (this["defaultDeposit"] != string.Empty)
                {
                    if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                    errorMessage += "Gép kaució nem megfelelő";
                }

                return errorMessage;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;
                switch (columnName)
                {
                    case "toolName":
                        errorMessage = ValidationRules.NameValidation(toolName);
                        break;

                    case "toolManufacturer":
                        errorMessage = ValidationRules.ManufacturerValidation(toolManufacturer);
                        break;

                    case "IDNumber":
                        errorMessage = ValidationRules.IDNumberValidation(IDNumber);
                        break;

                    case "serialNumber":
                        errorMessage = ValidationRules.SerialValidation(serialNumber);
                        break;

                    case "rentPrice":
                        errorMessage = ValidationRules.RentPriceValidation(rentPrice);
                        break;

                    case "fromDate":
                        errorMessage = ValidationRules.FromDateValidation(fromDate);
                        break;

                    case "defaultDeposit":
                        errorMessage = ValidationRules.DefaultDepositValidation(defaultDeposit);
                        break;
                }
                return errorMessage;
            }
        }
    }
}
