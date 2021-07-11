using ContactsBook.Domain.Exceptions;
using ContactsBook.Domain.UnitTests.Fixture.Email;
using ContactsBook.Domain.UnitTests.Fixture.PhoneNumber;
using ContactsBook.Domain.ValueObjects;
using Xunit;

namespace ContactsBook.Domain.UnitTests
{
    public class ValueObjectsTests
    {
        [Theory]
        [ClassData(typeof(ValidEmailsSetup))]
        public void CreateValidEmail_ShouldBeSuccess(string email)
        {
            var emailVO = new Email(email);

            Assert.Equal(emailVO.ToString(), email);
        }

        [Theory]
        [ClassData(typeof(InvalidEmailsSetup))]
        public void CreateInvalidEmail_ShouldThrow(string email)
        {
            Assert.Throws<InvalidEmailException>(() => new Email(email));
        }

        [Theory]
        [ClassData(typeof(ValidPhoneNumbersSetup))]
        public void CreateValidPhoneNumber_ShouldBeSuccess(long phoneNumber)
        {
            var phoneNumberVO = new PhoneNumber(phoneNumber);

            Assert.Equal(phoneNumber.ToString(), phoneNumberVO.ToString());
        }

        [Theory]
        [ClassData(typeof(InvalidPhoneNumbersSetup))]
        public void CreateInvalidPhoneNumber_ShouldThrow(long phoneNumber)
        {
            Assert.Throws<InvalidPhoneNumberException>(() => new PhoneNumber(phoneNumber));
        }
    }
}