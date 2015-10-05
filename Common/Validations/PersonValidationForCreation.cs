using Common.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Validations
{
    public class PersonValidationForCreation : ValidationRulesBase, IPersonValidation
    {
        public string AddressValidation(string address)
        {
            return string.Empty;
        }

        public string BirthDateValidation(DateTime? birthDate)
        {
            return string.Empty;
        }

        public string DefaultDiscountValidation(double? defaultDiscount)
        {
            return string.Empty;
        }

        public string IDNumberValidation(string IDNumber)
        {
            string errorMessage = CheckIfNullOrEmpty(IDNumber);
            return errorMessage;
        }

        public string MothersNameValidation(string mothersName)
        {
            return string.Empty;
        }

        public string NameValidation(string customerName)
        {
            string errorMessage = CheckIfNullOrEmpty(customerName);
            return errorMessage;
        }

        public string PhoneValidation(string phoneNumber)
        {
            return string.Empty;
        }

        public string WorkplaceValidation(string workplace)
        {
            return string.Empty;
        }
    }
}
