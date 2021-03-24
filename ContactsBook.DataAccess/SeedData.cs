using System;
using System.Collections.Generic;
using Bogus;
using ContactsBook.Domain.Entities;

namespace ContactsBook.DataAccess.MsSql
{
    public class SeedData
    {
        public List<Contact> SeedContacts { get; private set; }

        private Faker<Contact> _faker;

        public SeedData(Faker<Contact> faker)
        {
            SeedContacts = new List<Contact>();
            _faker = faker;
        }

        public SeedData Seed(int entitiesCount = 1000)
        {
            _faker.RuleFor(p => p.Id, _ => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Person.FullName)
                .RuleFor(p => p.Email.Value, f => f.Person.Email)
                .RuleFor(p => p.PhoneNumber.Value, f => f.Person.Random.Long(10000000000, 99999999999));

            var generated = _faker.Generate(entitiesCount);

            SeedContacts.AddRange(generated);

            return this;
        }
    }
}
