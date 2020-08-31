using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MachineInfo.ValidationRules
{
    public class ValidateIPAddress : ValidationRule
    {
        public IPAddressType Type { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //Validate if the string can be parsed as an IP
            string EvaluatedString = value.ToString();
            IPAddress ipAddress;
            if (IPAddress.TryParse(EvaluatedString, out ipAddress))
            {
                switch (Type)
                {
                    case IPAddressType.IPv4:
                        if (ipAddress.GetAddressBytes().Length != 4)
                        {
                            return new ValidationResult(false, "Not an IPv4 Address.");
                        }
                        break;
                    case IPAddressType.IPv6:
                        if (ipAddress.GetAddressBytes().Length != 16)
                        {
                            return new ValidationResult(false, "Not an IPv6 Address.");
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                return new ValidationResult(false, "Not a valid IPAddress.");
            }


            return ValidationResult.ValidResult;
        }

        public enum IPAddressType { IPv4, IPv6 }
    }
}
