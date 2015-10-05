using Common.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MiddleLayer.Representations
{
    public class FirmRepresentation : CustomerBaseRepresentation
    {
        private ObservableCollection<CustomerBaseRepresentation> _contacts;
        public ObservableCollection<CustomerBaseRepresentation> contacts
        {
            get { return _contacts; }
            set
            {
                if (_contacts != value)
                {
                    _contacts = value;
                    RaisePropertyChanged("contacts");
                }
            }
        }

        public FirmRepresentation()
        {
            this.isFirm = true;
        }

        public IFirmValidation ValidationRules { get; set; }

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
            if (this["IDNumber"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél azonosító nem megfelelő";
            }
            if (this["customerPhone"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél telefon nem megfelelő";
            }
            if (this["defaultDiscount"] != string.Empty)
            {
                if (errorMessage != string.Empty) errorMessage += Environment.NewLine;
                errorMessage += "Ügyfél kedvezmény nem megfelelő";
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
            }
            return errorMessage;
        }
    }
}
