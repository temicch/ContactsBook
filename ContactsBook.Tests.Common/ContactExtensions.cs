using System;
using System.Collections.Generic;
using System.Linq;
using ContactsBook.Application.Interfaces.Models;

namespace ContactsBook.Tests.Common
{
    public static class ContactExtensions
    {
        public static ContactDto WithPhone(this ContactDto contact, long phoneNumber)
        {
            contact.PhoneNumber = phoneNumber;
            return contact;
        }

        public static IEnumerable<ContactDto> WithPhones(this IEnumerable<ContactDto> contacts,
            IEnumerable<long> phones)
        {
            return contacts.Select((contact, i) =>
            {
                contact.PhoneNumber = phones.ElementAt(i);
                return contact;
            });
        }

        public static IEnumerable<ContactDto> WithNames(this IEnumerable<ContactDto> contacts,
            IEnumerable<string> names)
        {
            return contacts.Select((contact, i) =>
            {
                contact.Name = names.ElementAt(i);
                return contact;
            });
        }

        public static ContactDto WithName(this ContactDto contact, string name)
        {
            contact.Name = name;
            return contact;
        }

        public static ContactDto WithEmail(this ContactDto contact, string email)
        {
            contact.Email = email;
            return contact;
        }

        public static ContactDto WithId(this ContactDto contact, Guid id)
        {
            contact.Id = id;
            return contact;
        }
    }
}