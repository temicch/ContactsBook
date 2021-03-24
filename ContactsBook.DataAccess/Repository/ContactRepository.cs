using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoMapper;
using ContactsBook.Application;
using ContactsBook.DataAccess.MsSql.Repository.Helpers;
using ContactsBook.Domain.Entities;
using static ContactsBook.DataAccess.MsSql.Repository.Helpers.ContactRepositoryHelpers;

namespace ContactsBook.DataAccess.MsSql.Repository
{
    public class ContactRepository : IContactRepository<Contact>, IDisposable
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly SqlConnection _sqlConnection;
        private readonly string _tableName;

        public ContactRepository(string connectionString, IMapper mapper, string tableName = "Contacts")
        {
            _connectionString = connectionString;
            _mapper = mapper;
            _sqlConnection = new SqlConnection(connectionString);
            _tableName = tableName;

            //var q = new SeedData(new Bogus.Faker<Contact>()).Seed().SeedContacts;
            //AddRangeContactsAsync(q);
        }

        public async Task<Guid> InsertAsync(Contact contact)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                {
                    var contactId = await InsertContactAsync(_sqlConnection, contact);
                    contact.Id = contactId;

                    return contactId;
                }
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public async Task InsertAsync(IEnumerable<Contact> contacts)
        {
            await InsertContactsAsync(_connectionString, _tableName, contacts);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                await _sqlConnection.OpenAsync();

                return await RemoveContactAsync(_sqlConnection, id);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public async Task<bool> UpdateAsync(Contact contact)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                var contactId = await ContactRepositoryHelpers.UpdateContactAsync(_sqlConnection, contact);

                return contactId != default(Guid);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public async Task<Contact> GetByIdAsync(Guid id)
        {
            try
            {
                await _sqlConnection.OpenAsync();
                return await SelectContactAsync(_sqlConnection, _mapper, id);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public async Task<SelectResult<Contact>> GetAsync(LimitationParameters limitationParameters)
        {
            try
            {
                await _sqlConnection.OpenAsync();

                return await SelectAllContactsAsync(_sqlConnection, _mapper, limitationParameters);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public async Task<SelectResult<Contact>> GetByPhoneNumberAsync(string phoneNumber, LimitationParameters limitationParameters)
        {
            try
            {
                await _sqlConnection.OpenAsync();

                return await SelectPhoneContactsAsync(_sqlConnection, _mapper, phoneNumber, limitationParameters);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public async Task<SelectResult<Contact>> GetByNameAsync(string name, LimitationParameters limitationParameters)
        {
            try
            {
                await _sqlConnection.OpenAsync();

                return await SelectNameContactsAsync(_sqlConnection, _mapper, name, limitationParameters);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void Dispose()
        {
            _sqlConnection.Dispose();
        }

    }
}