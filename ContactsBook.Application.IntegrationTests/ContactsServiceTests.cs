using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Application.Interfaces.Services;
using ContactsBook.Application.PagedList;
using ContactsBook.DataAccess.MsSql;
using ContactsBook.Domain.Entities;
using ContactsBook.Infrastructure.Interfaces;
using ContactsBook.Tests.Common;
using ContactsBook.WebApi;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ContactsBook.Application.IntegrationTests
{
    public class ContactsServiceTests : IClassFixture<CbWebApplicationFactory<Startup>>, IDisposable
    {
        private readonly CbWebApplicationFactory<Startup> _cbWebApplicationFactory;
        private readonly HttpClient _client;
        private readonly ContactsDbContext _contactsDbContext;
        private readonly IContactsService _contactsService;
        internal readonly IFakeDataGenerator<Contact> _fakeDataGenerator;
        internal readonly IMapper _mapper;
        private readonly IServiceScope _serviceScope;

        public ContactsServiceTests(CbWebApplicationFactory<Startup> cbWebApplicationFactory)
        {
            _cbWebApplicationFactory = cbWebApplicationFactory;
            _client ??= _cbWebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _serviceScope = _cbWebApplicationFactory.ServiceProvider.CreateScope();
            _contactsService = _serviceScope.ServiceProvider.GetRequiredService<IContactsService>();
            _contactsDbContext = _serviceScope.ServiceProvider.GetRequiredService<ContactsDbContext>();
            _fakeDataGenerator = _serviceScope.ServiceProvider.GetRequiredService<IFakeDataGenerator<Contact>>();
            _mapper = _serviceScope.ServiceProvider.GetRequiredService<IMapper>();
        }

        public void Dispose()
        {
            _cbWebApplicationFactory.DbClear();
            _serviceScope.Dispose();
        }

        [Fact]
        public async Task Contact_With_The_Same_Phone_Cant_Be_Created()
        {
            // Assign
            var contacts = this.GetSomeContacts(2).WithPhones(new[] {91345678910, 91345678910}).ToList();

            // Act
            var isContactA_Created = await _contactsService.AddContactAsync(contacts[0]);
            var isContactB_Created = await _contactsService.AddContactAsync(contacts[1]);

            // Assert
            isContactA_Created.Should().NotBeEmpty();
            isContactB_Created.Should().BeEmpty();
        }

        [Fact]
        public async Task Created_Contact_Is_Finded()
        {
            // Assign
            var contactA = this.GetSomeContacts().ElementAt(0);

            // Act
            var contactAId = await _contactsService.AddContactAsync(contactA);
            contactA.Id = contactAId;
            var contactAFromDb = await _contactsService.GetContactByIdAsync(contactAId);

            // Assert
            contactA.Should().BeEquivalentTo(contactAFromDb);
        }

        [Fact]
        public async Task Created_Contacts_Is_Not_Equals()
        {
            // Assign
            var contacts = this.GetSomeContacts(2).ToList();

            // Act
            var contactAId = await _contactsService.AddContactAsync(contacts[0]);
            var contactBId = await _contactsService.AddContactAsync(contacts[1]);
            contacts[0].Id = contactAId;
            contacts[1].Id = contactBId;
            var contactAFromDb = await _contactsService.GetContactByIdAsync(contactAId);
            var contactBFromDb = await _contactsService.GetContactByIdAsync(contactBId);

            // Assert
            contacts[0].Should().BeEquivalentTo(contactAFromDb);
            contacts[1].Should().BeEquivalentTo(contactBFromDb);
            contacts[0].Should().NotBeEquivalentTo(contacts[1]);
        }

        [Fact]
        public async Task Removed_Contact_Is_Not_Finded()
        {
            // Assign
            var contactA = this.GetSomeContacts().ElementAt(0);

            // Act
            var contactAId = await _contactsService.AddContactAsync(contactA);
            var beforeDeleted = await _contactsService.GetContactByIdAsync(contactAId);
            var removeContactResult = await _contactsService.RemoveContactByIdAsync(contactAId);
            var getRemovedContactByIdResult = await _contactsService.GetContactByIdAsync(contactAId);
            var getRemovedContactByPhoneResult = await _contactsService
                .GetContactByPhoneNumberAsync(contactA.PhoneNumber.ToString());

            // Assert
            beforeDeleted.Should().NotBeNull();
            removeContactResult.Should().BeTrue();
            getRemovedContactByIdResult.Should().BeNull();
            getRemovedContactByPhoneResult.Should().BeNull();
        }

        [Fact]
        public async Task Returns_Empty_List_In_Empty_Db()
        {
            // Assign
            var dbItems = _contactsDbContext.Contacts.Count();

            // Act
            var contacts = await _contactsService.GetContactsAsync(new LimitationParameters());

            // Assert
            dbItems.Should().Be(0);
            contacts.TotalCount.Should().Be(0);
            contacts.Items.Should().BeEmpty();
        }

        [Fact]
        public async Task Remove_Non_Exists_Contact_Returns_False()
        {
            // Assign
            var fakeId = Guid.NewGuid();
            var contacts = await _contactsService.GetContactsAsync(new LimitationParameters());

            // Act
            var removeResult = await _contactsService.RemoveContactByIdAsync(fakeId);

            // Assert
            contacts.TotalCount.Should().Be(0);
            contacts.Items.Should().BeEmpty();
            removeResult.Should().BeFalse();
        }

        [Fact]
        public async Task Contact_Successfully_Updated()
        {
            // Assign
            var contactDto = new ContactDto().WithName("ContactA").WithPhone(79134446678);
            var contact = await _contactsService.AddContactAsync(contactDto);
            contactDto.Id = contact;
            var contactNewName = "UpdatedContact";
            var contactNewPhoneNumber = contactDto.PhoneNumber + 1;

            // Act
            contactDto.WithName(contactNewName).WithPhone(contactNewPhoneNumber);
            var updateResult = await _contactsService.UpdateContactAsync(contactDto);
            var updatedContact = await _contactsService
                .GetContactByPhoneNumberAsync(contactDto.PhoneNumber.ToString());

            // Assert
            contact.Should().NotBe(default);
            updateResult.Should().BeTrue();
            updatedContact.Name.Should().Be(contactNewName);
            updatedContact.PhoneNumber.Should().Be(contactNewPhoneNumber);
            updatedContact.Id.Should().Be(contact);
        }

        [Fact]
        public async Task Update_Contact_With_Existing_Phone_Number_Returns_False()
        {
            // Assign
            var contacts = this.GetSomeContacts(2).ToList();
            var contact1 = await _contactsService.AddContactAsync(contacts[0]);
            var contact2 = await _contactsService.AddContactAsync(contacts[1]);
            contacts[1].Id = contact2;

            // Act
            contacts[1].WithPhone(contacts[0].PhoneNumber);
            var updateContact2DtoResult = await _contactsService.UpdateContactAsync(contacts[1]);

            // Assert
            contact1.Should().NotBe(default);
            contact2.Should().NotBe(default);
            updateContact2DtoResult.Should().BeFalse();
        }

        [Fact]
        public async Task Find_Contacts_By_Part_Of_Phone_Number()
        {
            //Assign
            var phoneNumbers = new[] {70000000000, 71111111111, 70000010000};
            var contacts = this.GetSomeContacts(phoneNumbers.Length)
                .WithPhones(phoneNumbers);
            foreach (var contact in contacts)
                await _contactsService.AddContactAsync(contact);

            // Act
            var resultWithZeroes = await _contactsService
                .FindContactsByPhoneNumberAsync("0000", new LimitationParameters());
            var resultWithOnes = await _contactsService
                .FindContactsByPhoneNumberAsync("1111", new LimitationParameters());

            // Assert
            resultWithZeroes.TotalCount.Should().Be(2);
            resultWithOnes.TotalCount.Should().Be(1);
        }

        [Fact]
        public async Task Find_Contacts_By_Part_Of_Name()
        {
            // Assign
            var names = TestDataFactory.GetSomeNames("UniquePart", 3);
            var contactsWithUniqueParts = this.GetSomeContacts(names.Count())
                .WithNames(names);
            var contactsWithNonUniqueParts = this.GetSomeContacts(100, 50);
            foreach (var contact in contactsWithUniqueParts)
                await _contactsService.AddContactAsync(contact);
            foreach (var contact in contactsWithNonUniqueParts)
                await _contactsService.AddContactAsync(contact);

            // Act
            var findResult = await _contactsService.FindContactsByNameAsync("UniquePart", new LimitationParameters());
            var allResult = await _contactsService.GetContactsAsync(new LimitationParameters());

            // Assert
            findResult.TotalCount.Should().Be(3);
            allResult.TotalCount.Should().Be(103);
        }

        [Fact]
        public async Task Existing_Phone_Is_Finded()
        {
            // Assign
            var contact = this.GetSomeContacts().ToList()[0];

            // Act
            var contactAddResult = await _contactsService.AddContactAsync(contact);
            var isPhoneExists = await _contactsService.IsPhoneNumberExistsAsync(contact.PhoneNumber.ToString());

            //Assert
            contactAddResult.Should().NotBe(default);
            isPhoneExists.Should().BeTrue();
        }
    }
}