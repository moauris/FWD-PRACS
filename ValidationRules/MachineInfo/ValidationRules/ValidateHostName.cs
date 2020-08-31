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
    /// Validate HostName:
    /// Pattern 1: must start with 3 alphabets
    /// Pattern 2: after 3 digits, must be alphnumeric characters
    /// Length: must be between 8 to 50 characters
    /// 
    /// </summary>
    public class ValidateHostName : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string testString = value.ToString();
            Regex validRegex = new Regex(@"^[a-zA-z]{3}[a-zA-Z0-9]{5,47}$");
            if (!validRegex.IsMatch(testString))
            {
                return new ValidationResult(false, "Host Name must be a string, " +
                    "which starts with 3 alphabets and followed by alphnumeric combination no longer than 50 characters.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
