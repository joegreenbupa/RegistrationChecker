using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegistrationChecker.Helpers
{
    public static class StringHelper
    {
        public static string GetFormattedRegistration(string input)
        {
            // Clean up the input value & make case consitant
            string formattedRegistration = RemoveWhitespace(input);
            return formattedRegistration.ToUpper();
        }

        private static string RemoveWhitespace(string input)
        {
            // Remove any whitespace from the input
            string cleanedInput = string.Concat(input.Where(c => !char.IsWhiteSpace(c)));
            return cleanedInput;
        }
    }
}
