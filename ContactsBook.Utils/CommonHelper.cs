using System.Text.RegularExpressions;

namespace ContactsBook.Utils
{
    public static class CommonHelper
    {
        // From https://emailregex.com/
        private const string EMAIL_EXPRESSION = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        private static readonly Regex _emailRegex;

        static CommonHelper()
        {
            _emailRegex = new Regex(EMAIL_EXPRESSION, RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Verifies that a string is in valid e-mail format
        /// </summary>
        /// <param name="email">Email to verify</param>
        /// <returns>true if the string is a valid e-mail address and false if it's not</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return true;

            email = email.Trim();

            return _emailRegex.IsMatch(email);
        }

        /// <summary>
        ///     Verifies that a string is in valid phone number format
        /// </summary>
        /// <param name="email">Phone number to verify</param>
        /// <returns>true if the string is a valid phone number and false if it's not</returns>
        public static bool IsValidPhoneNumber(long phoneNumber)
        {
            return !((phoneNumber < 10000000000) | (phoneNumber > 99999999999));
        }
    }
}