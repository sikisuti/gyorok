using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Validations.Interfaces
{
    public interface IPersonValidation
    {
        string NameValidation(string customerName);
        string IDNumberValidation(string IDNumber);
        string MothersNameValidation(string mothersName);
        string BirthDateValidation(DateTime? birthDate);
        string AddressValidation(string address);
        string PhoneValidation(string phoneNumber);
        string WorkplaceValidation(string workplace);
        string DefaultDiscountValidation(double? defaultDiscount);
    }
}
