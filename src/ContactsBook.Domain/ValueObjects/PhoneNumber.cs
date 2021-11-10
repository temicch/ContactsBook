using ContactsBook.Domain.Exceptions;
using ContactsBook.Utils;

namespace ContactsBook.Domain.ValueObjects;

public record struct PhoneNumber
{
    public PhoneNumber(long value)
    {
        if (!CommonHelper.IsValidPhoneNumber(value))
            throw new InvalidPhoneNumberException();

        Value = value;
    }

    public long Value { get; init; }

    public override string ToString()
    {
        return Value.ToString();
    }
}
