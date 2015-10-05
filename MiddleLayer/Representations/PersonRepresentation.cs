using Common.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class PersonRepresentation : CustomerBaseRepresentation
    {
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

        public IPersonValidation ValidationRules { get; set; }

        public PersonRepresentation()
        {
            this.isFirm = false;
        }

        protected override string GetError()
        {
            if (ValidationRules == null)
                return "Nincs ügyfél érvényességi szabály";

            string errorMessage = string.Empty;

            if (this["customerName"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél név nem megfelelő";
            }
            if (this["customerAddress"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél cím nem megfelelő";
            }
            if (this["customerName"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél azonosító nem megfelelő";
            }
            if (this["customerName"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél telefon nem megfelelő";
            }
            if (this["customerName"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél kedvezmény nem megfelelő";
            }
            if (this["mothersName"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél anyja neve nem megfelelő";
            }
            if (this["birthDate"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél születési dátum nem megfelelő";
            }
            if (this["workplace"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél munkahely nem megfelelő";
            }

            return errorMessage;
        }

        protected override string GetValidation(string columnName)
        {
            string errorMessage = string.Empty;
            switch (columnName)
            {
                case "customerName":
                    errorMessage = ValidationRules.NameValidation(customerName);
                    break;

                case "customerAddress":
                    errorMessage = ValidationRules.AddressValidation(customerAddress);
                    break;

                case "IDNumber":
                    errorMessage = ValidationRules.IDNumberValidation(IDNumber);
                    break;

                case "customerPhone":
                    errorMessage = ValidationRules.PhoneValidation(customerPhone);
                    break;

                case "defaultDiscount":
                    errorMessage = ValidationRules.DefaultDiscountValidation(defaultDiscount);
                    break;

                case "mothersName":
                    errorMessage = ValidationRules.MothersNameValidation(mothersName);
                    break;

                case "birthDate":
                    errorMessage = ValidationRules.BirthDateValidation(birthDate);
                    break;

                case "workplace":
                    errorMessage = ValidationRules.WorkplaceValidation(workplace);
                    break;
            }
            return errorMessage;
        }        
    }
}
