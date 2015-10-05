using Common.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Validations
{
    public class PersonValidationForRent : ValidationRulesBase, IPersonValidation
    {
        public string AddressValidation(string address)
        {
            string errorMessage = CheckIfNullOrEmpty(address);
            return errorMessage;
        }

        public string BirthDateValidation(DateTime? birthDate)
        {
            string errorMessage = string.Empty;
            return errorMessage;
        }

        public string DefaultDiscountValidation(double? defaultDiscount)
        {
            string errorMessage = string.Empty;
            return errorMessage;
        }

        public string IDNumberValidation(string IDNumber)
        {
            string errorMessage = CheckIfNullOrEmpty(IDNumber);
            return errorMessage;
        }

        public string MothersNameValidation(string mothersName)
        {
            string errorMessage = CheckIfNullOrEmpty(mothersName);
            return errorMessage;
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
            string errorMessage = CheckIfNullOrEmpty(workplace);
            return errorMessage;
        }
    }
}
