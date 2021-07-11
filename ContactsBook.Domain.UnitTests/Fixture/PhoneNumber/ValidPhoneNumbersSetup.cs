using Xunit;

namespace ContactsBook.Domain.UnitTests.Fixture.PhoneNumber
{
    public class ValidPhoneNumbersSetup : TheoryData<long>
    {
        public ValidPhoneNumbersSetup()
        {
            Add(99999999999);
            Add(10000000000);
            Add(98784111240);
            Add(11123354732);
            Add(55555555511);
        }
    }
}