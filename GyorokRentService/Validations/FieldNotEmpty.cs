using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace GyorokRentService.Validations
{
    public class FieldNotEmpty : ValidationRule
    {
        public FieldNotEmpty() : base()
        {
            ValidatesOnTargetUpdated = true;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null || value.ToString() == string.Empty)
                return new ValidationResult(false, "Kötelező!");

            return ValidationResult.ValidResult;
        }
    }
}
