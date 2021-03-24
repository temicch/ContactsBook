using ContactsBook.Utils;
using System;

namespace ContactsBook.Domain.ValueObjects
{
    public record PhoneNumber
    {

        public long Value { get; init; }

        public PhoneNumber(long value)
        {
            if (!CommonHelper.IsValidPhoneNumber(value))
            {
                throw new Exception(string.Format("Phone number {0} is not valid", value));
            }

            Value = value;
        }
    }
}