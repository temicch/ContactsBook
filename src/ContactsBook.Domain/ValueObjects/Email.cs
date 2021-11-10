using ContactsBook.Domain.Exceptions;
using ContactsBook.Utils;

namespace ContactsBook.Domain.ValueObjects;

public record Email
{
    public Email(string value)
    {
        if (!CommonHelper.IsValidEmail(value))
            throw new InvalidEmailException();

        Value = value;
    }

    public string Value { get; init; }

    public override string ToString()
    {
        return Value;
    }
}
