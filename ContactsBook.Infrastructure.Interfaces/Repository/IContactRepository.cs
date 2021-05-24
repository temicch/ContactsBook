using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsBook.Application.Interfaces.PagedList;
using ContactsBook.Domain.Entities;
using ContactsBook.Infrastructure.Interfaces.SelectResult;

namespace ContactsBook.Infrastructure.Interfaces.Repository
{
    public interface IContactRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        ///     Add new contact
        /// </summary>
        /// <param name="entity">Contact</param>
        /// <returns>Id of added entity. If such entity can not be created, default value will be returned</returns>
        Task<Guid> InsertAsync(TEntity entity);

        /// <summary>
        ///     Add new contacts
        /// </summary>
        /// <param name="contacts">Contacts</param>
        Task InsertAsync(IEnumerable<TEntity> contacts);

        /// <summary>
        ///     Remove contact by id
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> DeleteByIdAsync(Guid id);

        /// <summary>
        ///     Update contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> UpdateAsync(TEntity contact);

        /// <summary>
        ///     Get contact by id
        /// </summary>
        /// <param name="id">Contact</param>
        /// <returns>Contact entity if such exists, null otherwise</returns>
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        ///     Get contacts by the specified person name. This method searches among partial matches by person name.
        /// </summary>
        /// <param name="name">Partially specified name</param>
        /// <param name="limitationParameters">Parameters for limit select</param>
        Task<ISelectResult<TEntity>> GetByNameAsync(string name, ILimitationParameters limitationParameters);

        /// <summary>
        ///     Get contacts by the specified phone number. This method searches among partial matches by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="limitationParameters">Parameters for limit select</param>
        Task<ISelectResult<TEntity>> GetByPhoneNumberAsync(string phoneNumber,
            ILimitationParameters limitationParameters);

        /// <summary>
        ///     Get all contacts with limit
        /// </summary>
        /// <param name="limitationParameters">Parameters for limit select</param>
        Task<ISelectResult<TEntity>> GetAsync(ILimitationParameters limitationParameters);
    }
}