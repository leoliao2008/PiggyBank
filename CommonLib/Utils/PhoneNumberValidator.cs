using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLib.Utils
{
    public class PhoneNumberValidator
    {
        public static bool IsValidPhoneNumber(string phoneNumber) 
        {
            return Regex.IsMatch(phoneNumber,"^1[3458][0-9]{9}$");
        }
    }
}
