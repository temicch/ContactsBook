using System;
using ContactsBook.Utils;

namespace ContactsBook.Domain.ValueObjects
{
    public record Email
    {
        public Email(string value)
        {
            if (!CommonHelper.IsValidEmail(value)) throw new Exception($"Email {value} is not valid");

            Value = value;
        }

        public string Value { get; init; }
    }
}