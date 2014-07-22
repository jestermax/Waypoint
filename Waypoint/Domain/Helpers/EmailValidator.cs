using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain.Helpers
{
    public static class EmailValidator
    {
        private static readonly List<string> DisallowedEmailAddresses = new List<string>
        {
            "alexsimms@gmail.com"
        };

        private static readonly List<string> DisallowedDomains = new List<string>
        {
            "trackem.com", "trackem.ca", "solutionsintomotion.com", "geotab.com"
        };

        public static bool IsValid(string emailAddress)
        {
            if (String.IsNullOrEmpty(emailAddress) || String.IsNullOrWhiteSpace(emailAddress))
            {
                return false;
            }

            if (emailAddress.StartsWith("."))
            {
                return false;
            }

            const string expression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            var regex = new Regex(expression);

            if (!regex.IsMatch(emailAddress))
            {
                return false;
            }

            if (DisallowedEmailAddresses.Contains(emailAddress.ToLower()))
            {
                return false;
            }

            try
            {
                var split = emailAddress.Split(new[] { '@' });

                if (split.Length != 2)
                {
                    return false;
                }

                return !DisallowedDomains.Contains(split[1].ToLower());
            }
            catch
            {
                return false;
            }
        }
    }
}
