using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsBook.Domain.Entities;

namespace ContactsBook.Application
{
    public interface IContactRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity">Contact</param>
        /// <returns>Id of added entity</returns>
        Task<Guid> InsertAsync(TEntity entity);
        /// <summary>
        /// Add new contacts
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>Id of added entity</returns>
        Task InsertAsync(IEnumerable<TEntity> contact);
        /// <summary>
        /// Remove contact by id
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> DeleteByIdAsync(Guid id);
        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> UpdateAsync(TEntity contact);
        /// <summary>
        /// Get contact by id
        /// </summary>
        /// <param name="id">Contact</param>
        /// <returns>Contact entity if such exists, null otherwise</returns>
        Task<TEntity> GetByIdAsync(Guid id);
        /// <summary>
        /// Get contacts by the specified person name. This method searches among partial matches by person name.
        /// </summary>
        /// <param name="name">Partially specified name</param>
        /// <param name="limitationParameters">Parameters for limit select</param>
        Task<SelectResult<TEntity>> GetByNameAsync(string name, LimitationParameters limitationParameters);

        /// <summary>
        /// Get contacts by the specified phone number. This method searches among partial matches by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="limitationParameters">Parameters for limit select</param>
        Task<SelectResult<TEntity>> GetByPhoneNumberAsync(string phoneNumber, LimitationParameters limitationParameters);
        /// <summary>
        /// Get all contacts with limit
        /// </summary>
        /// <param name="limitationParameters">Parameters for limit select</param>
        Task<SelectResult<TEntity>> GetAsync(LimitationParameters limitationParameters);
    }
}
