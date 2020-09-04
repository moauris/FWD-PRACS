using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MachineInfo.ValidationRules
{
    class ValidateOScbxEmpty : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((string)value == string.Empty)
            {
                return new ValidationResult(false, "Combo Box cannot be empty.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
