using System;

namespace ContactsBook.Domain.Exceptions
{
    public class InvalidEmailException: ArgumentException
    {
        public override string ParamName { get; } = "Email";

        public InvalidEmailException(): base()
        {

        }
    }
}
