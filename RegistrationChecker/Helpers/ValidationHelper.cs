using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegistrationChecker.Helpers
{
    public static class ValidationHelper
    {
        public static bool ValidateRegistration(string registrationNumber)
        {
            // Check if string matches a known existing licence plate format
            string registrationRegex = "(^[A-Z]{2}[0-9]{2}s?[A-Z]{3}$)" +   // Current
                                       "|(^[A-Z][0-9]{1,3}[A-Z]{3}$)" +     // Prefix
                                       "|(^[A-Z]{3}[0-9]{1,3}[A-Z]$)" +     // Suffix
                                       "|(^[0-9]{1,4}[A-Z]{1,2}$)" +        // DatelessLongNumberPrefix
                                       "|(^[0-9]{1,3}[A-Z]{1,3}$)" +        // DatelessShortNumberPrefix
                                       "|(^[A-Z]{1,2}[0-9]{1,4}$)" +        // DatelessLongNumberSuffix
                                       "|(^[A-Z]{1,3}[0-9]{1,3}$)" +        // DatelessShortNumberSuffix
                                       "|(^[A-Z]{1,3}[0-9]{1,4}$)" +        // DatelessNorthernIreland
                                       "|(^[0-9]{3}[DX]{1}[0-9]{3}$)";      // DiplomaticPlate

            Match match = Regex.Match(registrationNumber, registrationRegex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return true;
            }

            return false; 
        }
    }
}
