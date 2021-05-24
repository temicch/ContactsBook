using System;
using ContactsBook.Utils;

namespace ContactsBook.Domain.ValueObjects
{
    public record PhoneNumber
    {
        public PhoneNumber(long value)
        {
            if (!CommonHelper.IsValidPhoneNumber(value))
                throw new Exception($"Phone number {value} is not valid");

            Value = value;
        }

        public long Value { get; init; }
    }
}