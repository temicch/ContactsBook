using System;

namespace ContactsBook.Domain.Exceptions
{
    public class InvalidPhoneNumberException : ArgumentException
    {
        public override string ParamName { get; } = "Phone number";
    }
}