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
        public void Valid_Email_Is_Created(string email)
        {
            var emailVO = new Email(email);

            Assert.Equal(emailVO.ToString(), email);
        }

        [Theory]
        [ClassData(typeof(InvalidEmailsSetup))]
        public void Invalid_Email_Is_Throw_Exception(string email)
        {
            Assert.Throws<InvalidEmailException>(() => new Email(email));
        }

        [Theory]
        [ClassData(typeof(ValidPhoneNumbersSetup))]
        public void Valid_PhoneNumber_Is_Created(long phoneNumber)
        {
            var phoneNumberVO = new PhoneNumber(phoneNumber);

            Assert.Equal(phoneNumber.ToString(), phoneNumberVO.ToString());
        }

        [Theory]
        [ClassData(typeof(InvalidPhoneNumbersSetup))]
        public void Invalid_Phone_Is_Throw_Exception(long phoneNumber)
        {
            Assert.Throws<InvalidPhoneNumberException>(() => new PhoneNumber(phoneNumber));
        }
    }
}