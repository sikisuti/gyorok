using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Validations.Interfaces
{
    public interface IToolValidationRules
    {
        string NameValidation(string toolName);
        string ManufacturerValidation(string toolManufacturer);
        string IDNumberValidation(string IDNumber);
        string SerialValidation(string serialNumber);
        string RentPriceValidation(long rentPrice);
        string FromDateValidation(DateTime fromDate);
        string DefaultDepositValidation(long? defaultDeposit);
    }
}
