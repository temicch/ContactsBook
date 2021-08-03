using System;
using System.Collections.Generic;
using System.Linq;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Domain.ValueObjects;
using ContactsBook.Utils;

namespace ContactsBook.Application.IntegrationTests
{
    public static class TestDataFactory
    {
        public static IEnumerable<long> GetSomePhoneNumbers(int count = 1)
        {
            return Enumerable
                .Range(0, count)
                .Select(_ =>
                    (long) (CommonHelper.MIN_VALID_PHONE_NUMBER +
                            new Random().NextDouble() * CommonHelper.MAX_VALID_PHONE_NUMBER));
        }

        public static IEnumerable<string> GetSomeNames(string pattern = "", int count = 1)
        {
            if (pattern == null)
                pattern = string.Empty;

            return Enumerable
                .Range(0, count)
                .Select(i =>
                {
                    var randPosition = new Random().Next(0, 28 - pattern.Length);
                    var str = Guid.NewGuid().ToString().Insert(randPosition, pattern).Remove(28);
                    return str;
                });
        }

        public static IEnumerable<ContactDto> GetSomeContacts(this ContactsServiceTests contactsServiceTests,
            int count = 1, long phoneOffset = 0)
        {
            return contactsServiceTests._fakeDataGenerator.Generate(count)
                .Select((contact, i) =>
                {
                    contact.Id = Guid.Empty;
                    contact.Name = Guid.NewGuid().ToString().Remove(28);
                    contact.PhoneNumber = new PhoneNumber(phoneOffset + CommonHelper.MIN_VALID_PHONE_NUMBER + i);
                    return contactsServiceTests._mapper.Map<ContactDto>(contact);
                });
        }
    }
}