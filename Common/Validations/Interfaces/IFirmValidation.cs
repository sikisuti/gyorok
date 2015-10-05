using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Common.Validations.Interfaces
{
    public interface IFirmValidation
    {
        string NameValidation(string customerName);
        string IDNumberValidation(string IDNumber);
        string AddressValidation(string address);
        string PhoneValidation(string phoneNumber);
        string DefaultDiscountValidation(double? defaultDiscount);
    }
}
