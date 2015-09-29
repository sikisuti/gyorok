using Common.Validations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Validations
{
    public class ToolValidationRules : ValidationRulesBase, IToolValidationRules
    {
        public string DefaultDepositValidation(long? defaultDeposit)
        {

            string errorMessage = string.Empty;
            return errorMessage;
        }

        public string FromDateValidation(DateTime fromDate)
        {
            return string.Empty;
        }

        public string IDNumberValidation(string IDNumber)
        {
            string errorMessage = CheckIfNullOrEmpty(IDNumber);
            return errorMessage;
        }

        public string ManufacturerValidation(string toolManufacturer)
        {
            string errorMessage = CheckIfNullOrEmpty(toolManufacturer);
            return errorMessage;
        }

        public string NameValidation(string toolName)
        {
            string errorMessage = CheckIfNullOrEmpty(toolName);
            return errorMessage;
        }

        public string RentPriceValidation(long rentPrice)
        {
            string errorMessage = string.Empty;
            return errorMessage;
        }

        public string SerialValidation(string serialNumber)
        {
            string errorMessage = CheckIfNullOrEmpty(serialNumber);
            return errorMessage;
        }
    }
}
