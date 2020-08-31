using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MachineInfo.ValidationRules
{
    /// <summary>
    /// Validate Domain Input:
    /// Pattern: must start with .
    /// Must be consisted of at least two repeatition of .xxx combinations
    /// can be alphabet or numeric.
    /// Length: must be longer than 2 digits.
    /// </summary>
    class ValidateDomain : ValidationRule
    {
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string evaluatedString = value.ToString();
            Regex evalutationRegex = new Regex(@"^(\.[a-zA-Z0-9]{2,}){2,}$");
            if (!evalutationRegex.IsMatch(evaluatedString))
            {
                return new ValidationResult(false, "Not valid Domain Name: Must start with a dot character, containing alphanumeric values no less than 2 characters, repeating at least 2 times.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
