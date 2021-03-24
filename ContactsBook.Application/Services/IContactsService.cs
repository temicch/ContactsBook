using ContactsBook.Application.Models;
using System;
using System.Threading.Tasks;

namespace ContactsBook.Application.Services
{
    public interface IContactsService
    {
        /// <summary>
        /// Add new contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>Id of added entity</returns>
        Task<Guid> AddContactAsync(ContactDto contact);
        /// <summary>
        /// Remove contact by id
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> RemoveContactByIdAsync(Guid id);
        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> UpdateContactAsync(ContactDto contact);
        /// <summary>
        /// Get contact by id
        /// </summary>
        /// <param name="id">Contact</param>
        /// <returns>Contact entity if such exists, null otherwise</returns>
        Task<ContactDto> GetContactByIdAsync(Guid id);
        /// <summary>
        /// Get contacts by the specified phone number. This method searches among partial matches by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<PagedList<ContactDto>> GetContactsByPhoneNumberAsync(string phoneNumber, LimitationParameters limitationParameters);
        /// <summary>
        /// Get contacts
        /// </summary>
        /// <returns>Paginated result with <seealso cref="ContactDto"/> items</returns>
        Task<PagedList<ContactDto>> GetContactsAsync(LimitationParameters limitationParameters);
        /// <summary>
        /// Get contacts with filters. Only one will be expected
        /// </summary>
        /// <param name="limitationParameters"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<PagedList<ContactDto>> GetContactsAsync(LimitationParameters limitationParameters, string phoneNumber, string name);

        /// <summary>
        /// Get contacts by the specified person name. This method searches among partial matches by person name.
        /// </summary>
        /// <param name="name">Person name</param>
        /// <returns></returns>
        Task<PagedList<ContactDto>> GetContactsByNameAsync(string name, LimitationParameters limitationParameters);
    }
}
