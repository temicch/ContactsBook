using System;
using System.Collections.Generic;
using Bogus;
using ContactsBook.Domain.Entities;
using ContactsBook.Domain.ValueObjects;
using ContactsBook.Infrastructure.Interfaces;
using ContactsBook.Utils;

namespace ContactsBook.DataAccess.MsSql;

public class ContactsSeed : IFakeDataGenerator<Contact>
{
    private readonly Faker<Contact> _faker;

    public ContactsSeed(Faker<Contact> faker)
    {
        _faker = faker;
    }

    public IEnumerable<Contact> Generate(int count)
    {
        _faker.RuleFor(p => p.Id, _ => Guid.NewGuid())
            .RuleFor(p => p.Name, f => f.Person.FullName)
            .RuleFor(p => p.Email, f => new Email(f.Person.Email))
            .RuleFor(p => p.PhoneNumber,
                f => new PhoneNumber(f.Random.Long(CommonHelper.MIN_VALID_PHONE_NUMBER,
                    CommonHelper.MAX_VALID_PHONE_NUMBER)));

        return _faker.GenerateLazy(count);
    }
}
