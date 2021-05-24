using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsBook.Application.Interfaces.PagedList;
using ContactsBook.DataAccess.MsSql.Extensions;
using ContactsBook.Domain.Entities;
using ContactsBook.Infrastructure.Interfaces.Repository;
using ContactsBook.Infrastructure.Interfaces.SelectResult;

namespace ContactsBook.DataAccess.MsSql.Repository
{
    public class ContactRepository : IContactRepository<Contact>
    {
        private readonly ContactsDbContext _dbContext;

        public ContactRepository(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> InsertAsync(Contact contact)
        {
            if (await _dbContext.IsPhoneNumberExistAsync(contact.PhoneNumber.Value.ToString()))
                return default;

            return await _dbContext.InsertAsync(contact);
        }

        public async Task InsertAsync(IEnumerable<Contact> contacts)
        {
            await _dbContext.InsertAsync(contacts);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await _dbContext.DeleteByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Contact contact)
        {
            return await _dbContext.UpdateAsync(contact);
        }

        public async Task<Contact> GetByIdAsync(Guid id)
        {
            return await _dbContext.GetByIdAsync(id);
        }

        public async Task<ISelectResult<Contact>> GetAsync(ILimitationParameters limitationParameters)
        {
            return await _dbContext.GetAsync(limitationParameters);
        }

        public async Task<ISelectResult<Contact>> GetByPhoneNumberAsync(string phoneNumber,
            ILimitationParameters limitationParameters)
        {
            return await _dbContext.GetByPhoneNumberAsync(phoneNumber, limitationParameters);
        }

        public async Task<ISelectResult<Contact>> GetByNameAsync(string name,
            ILimitationParameters limitationParameters)
        {
            return await _dbContext.GetByNameAsync(name, limitationParameters);
        }
    }
}