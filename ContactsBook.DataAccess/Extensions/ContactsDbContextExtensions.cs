using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ContactsBook.Application.Interfaces.PagedList;
using ContactsBook.DataAccess.MsSql.Configurations;
using ContactsBook.DataAccess.MsSql.SelectResult;
using ContactsBook.Domain.Entities;
using ContactsBook.Domain.ValueObjects;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace ContactsBook.DataAccess.MsSql.Extensions
{
    internal static class ContactsDbContextExtensions
    {
        private const string SPLIT_PARAMETER = "PhoneNumber, Email";
        internal static readonly string tableName = ContactsConfig.TABLE_NAME;

        public static async Task<Guid> InsertAsync(this ContactsDbContext dbContext, Contact contact)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var contactId = await dbConnection.ExecuteScalarAsync<Guid>(
                $"INSERT INTO {tableName} (Name, Email, PhoneNumber) output INSERTED.ID VALUES (@Name, @Email, @PhoneNumber)",
                new
                {
                    contact.Name,
                    Email = contact.Email.Value,
                    PhoneNumber = contact.PhoneNumber.Value
                });
            contact.Id = contactId;

            return contactId;
        }

        public static async Task InsertAsync(this ContactsDbContext dbContext, IEnumerable<Contact> contacts)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var result = await dbConnection.ExecuteAsync(
                $"INSERT INTO {tableName} (Name, Email, PhoneNumber) VALUES (@Name, @Email, @PhoneNumber)", contacts
                    .Select(x => new
                    {
                        x.Name,
                        Email = x.Email.Value,
                        PhoneNumber = x.PhoneNumber.Value
                    }).ToList());
        }

        public static async Task<bool> DeleteByIdAsync(this ContactsDbContext dbContext, Guid id)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var result = await dbConnection.ExecuteAsync($"DELETE FROM {tableName} WHERE Id = @Id", new {Id = id});

            return result > 0;
        }

        public static async Task<bool> UpdateAsync(this ContactsDbContext dbContext, Contact contact)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var result = await dbConnection.ExecuteAsync(
                $"UPDATE {tableName} SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber where Id = @Id", new
                {
                    contact.Id,
                    contact.Name,
                    contact.Email,
                    contact.PhoneNumber
                });

            return result > 0;
        }

        public static async Task<Contact> GetByIdAsync(this ContactsDbContext dbContext, Guid id)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var contact = await dbConnection.QueryAsync<Contact, long, string, Contact>(
                $"SELECT Id, Name, PhoneNumber, Email from {tableName} WHERE Id = @Id",
                (contact, phoneNumber, email) =>
                {
                    contact.Email = new Email(email);
                    contact.PhoneNumber = new PhoneNumber(phoneNumber);

                    return contact;
                }, new {Id = id}, splitOn: SPLIT_PARAMETER);

            return contact.FirstOrDefault();
        }

        public static async Task<SelectResult<Contact>> GetAsync(this ContactsDbContext dbContext,
            ILimitationParameters limitationParameters)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var totalCount = await dbConnection.Count();
            var list = await dbConnection.QueryAsync<Contact, long, string, Contact>(
                $"SELECT Id, Name, PhoneNumber, Email from {tableName} {limitationParameters.GetMSSqlAddition()}",
                (contact, phoneNumber, email) =>
                {
                    contact.Email = new Email(email);
                    contact.PhoneNumber = new PhoneNumber(phoneNumber);

                    return contact;
                }, splitOn: SPLIT_PARAMETER);

            return new SelectResult<Contact>(list.AsList(), totalCount);
        }

        public static async Task<SelectResult<Contact>> GetByPhoneNumberAsync(this ContactsDbContext dbContext,
            string phoneNumber,
            ILimitationParameters limitationParameters)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var totalCount = await dbConnection.Count("WHERE PhoneNumber LIKE @PhoneNumber",
                new {PhoneNumber = $"%{phoneNumber}%"});
            var list = await dbConnection.QueryAsync<Contact, long, string, Contact>(
                $"SELECT Id, Name, PhoneNumber, Email from {tableName} WHERE PhoneNumber LIKE @PhoneNumber {limitationParameters.GetMSSqlAddition()}",
                (contact, phoneNumber, email) =>
                {
                    contact.Email = new Email(email);
                    contact.PhoneNumber = new PhoneNumber(phoneNumber);

                    return contact;
                }, new {PhoneNumber = $"%{phoneNumber}%"}, splitOn: SPLIT_PARAMETER);

            return new SelectResult<Contact>(list.AsList(), totalCount);
        }

        public static async Task<bool> IsPhoneNumberExistAsync(this ContactsDbContext dbContext, string phoneNumber)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var totalCount = await dbConnection.Count("WHERE PhoneNumber LIKE @PhoneNumber",
                new {PhoneNumber = $"%{phoneNumber}%"});

            return totalCount > 0;
        }

        public static async Task<SelectResult<Contact>> GetByNameAsync(this ContactsDbContext dbContext,
            string name,
            ILimitationParameters limitationParameters)
        {
            var dbConnection = dbContext.Database.GetDbConnection();

            var totalCount = await dbConnection.Count("WHERE Name LIKE @Name", new {Name = $"%{name}%"});
            var list = await dbConnection.QueryAsync<Contact, long, string, Contact>(
                $"SELECT Id, Name, PhoneNumber, Email from {tableName} WHERE Name LIKE @Name {limitationParameters.GetMSSqlAddition()}",
                (contact, phoneNumber, email) =>
                {
                    contact.Email = new Email(email);
                    contact.PhoneNumber = new PhoneNumber(phoneNumber);

                    return contact;
                }, new {Name = $"%{name}%"}, splitOn: SPLIT_PARAMETER);

            return new SelectResult<Contact>(list.AsList(), totalCount);
        }

        private static async Task<int> Count(this DbConnection dbConnection, string filter = "",
            object parameters = null)
        {
            return await dbConnection.QuerySingleOrDefaultAsync<int>($"SELECT COUNT(*) FROM {tableName} {filter}",
                parameters);
        }
    }
}