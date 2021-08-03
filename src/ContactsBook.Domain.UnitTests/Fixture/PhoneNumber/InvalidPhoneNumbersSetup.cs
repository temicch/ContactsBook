using Xunit;

namespace ContactsBook.Domain.UnitTests.Fixture.PhoneNumber
{
    public class InvalidPhoneNumbersSetup : TheoryData<long>
    {
        public InvalidPhoneNumbersSetup()
        {
            Add(5555555550554789012);
            Add(55555555505547890);
            Add(5555555550554789);
            Add(555555555055478);
            Add(55555555505547);
            Add(5555555550554);
            Add(555555555055);
            Add(5555555550);
            Add(9999999999);
            Add(0000000000);
            Add(57789011);
            Add(53);
            Add(11);
            Add(5);
        }
    }
}