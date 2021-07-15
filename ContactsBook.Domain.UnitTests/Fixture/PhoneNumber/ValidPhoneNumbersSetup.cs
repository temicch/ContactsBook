using ContactsBook.Utils;
using Xunit;

namespace ContactsBook.Domain.UnitTests.Fixture.PhoneNumber
{
    public class ValidPhoneNumbersSetup : TheoryData<long>
    {
        public ValidPhoneNumbersSetup()
        {
            Add(CommonHelper.MAX_VALID_PHONE_NUMBER);
            Add(CommonHelper.MIN_VALID_PHONE_NUMBER);
            Add(98784111240);
            Add(11123354732);
            Add(55555555511);
        }
    }
}