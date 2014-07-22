using System;
using System.Collections.Generic;

using Domain.Configuration;

namespace Domain.Helpers
{
    public static class PasswordValidator
    {
        private static readonly List<string> DisallowedPasswords = new List<string>
        {
            "password", "waypoint", "abc123", "123456", "12345", "qwerty", "letmein",
            "12345678", "welcome", "master", "123123", "iloveyou", "password1"
        };

        public static bool IsValid(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return false;
            }

            password = password.Trim();

            if (String.IsNullOrEmpty(password))
            {
                return false;
            }

            if (password.Length < AppConfiguration.UserMinimumPasswordLength)
            {
                return false;
            }

            if (password.Length > AppConfiguration.UserMaximumPasswordLength)
            {
                return false;
            }

            return !DisallowedPasswords.Contains(password);
        }
    }
}
