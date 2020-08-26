using Application.DAL.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.DAL.Repository
{
    public interface IContactRepository
    {
        /// <summary>
        /// Add new contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>Id of added entity</returns>
        Task<int> AddContactAsync(IContact contact);
        /// <summary>
        /// Remove contact by id
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> RemoveContactByIdAsync(int id);
        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <param name="contact">Contact</param>
        /// <returns>True if success, false otherwise</returns>
        Task<bool> UpdateContactAsync(int id, IContact contact);
        /// <summary>
        /// Get contact by id
        /// </summary>
        /// <param name="id">Contact</param>
        /// <returns>Contact entity if such exists, null otherwise</returns>
        Task<IContact> GetContactByIdAsync(int id);
        /// <summary>
        /// Get contacts by the specified email. This method searches among partial matches by email.
        /// </summary>
        /// <param name="email">Partially specified email</param>
        /// <returns></returns>
        Task<IEnumerable<IContact>> GetContactsByEmailAsync(string email);
        /// <summary>
        /// Get contacts by the specified person name. This method searches among partial matches by person name.
        /// </summary>
        /// <param name="name">Partially specified name</param>
        /// <returns></returns>
        Task<IEnumerable<IContact>> GetContactsByNameAsync(string name);

        /// <summary>
        /// Get contacts by the specified phone number. This method searches among partial matches by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<IEnumerable<IContact>> GetContactsByPhoneNumberAsync(string phoneNumber);
        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>Contacts</returns>
        Task<IEnumerable<IContact>> GetAllContactsAsync();
    }
}
