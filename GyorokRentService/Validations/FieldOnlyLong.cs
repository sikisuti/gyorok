using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace GyorokRentService.Validations
{
    public class FieldOnlyLong : ValidationRule
    {
        public FieldOnlyLong() : base()
        {
            ValidatesOnTargetUpdated = true;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            long num;
            if (value != null && !long.TryParse(value.ToString(), out num))
                return new ValidationResult(false, "Csak egész szám lehet!");

            return ValidationResult.ValidResult;
        }
    }
}
