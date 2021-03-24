using ContactsBook.Utils;
using System;

namespace ContactsBook.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; init; }

        public Email(string value)
        {
            if(!CommonHelper.IsValidEmail(value))
            {
                throw new Exception(string.Format("Email {0} is not valid", value));
            }

            Value = value;
        }
    }
}