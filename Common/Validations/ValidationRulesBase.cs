using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Validations
{
    public class ValidationRulesBase
    {
        public static string CheckIfNullOrEmpty(string property)
        {
            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(property))
            {
                errorMessage = "Kötelező";
            }
            return errorMessage;
        }

        public static string CheckIfValidPercent(long number)
        {
            string errorMessage = string.Empty;
            if (number < 0 || number > 100)
            {
                errorMessage = "0 - 100";
            }
            return errorMessage;
        }
    }
}
