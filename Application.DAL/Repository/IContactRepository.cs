using Application.DAL.Entity;
using System.Collections.Generic;

namespace Application.DAL.Repository
{
    public interface IContactRepository
    {
        /// <summary>
        /// Add new contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>Id of added entity</returns>
        int AddContact(IContact contact);
        /// <summary>
        /// Remove contact by id
        /// </summary>
        /// <param name="id">Contact id</param>
        /// <returns>True if success, false otherwise</returns>
        bool RemoveContactById(int id);
        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>True if success, false otherwise</returns>
        bool UpdateContact(IContact contact);
        /// <summary>
        /// Get contact by id
        /// </summary>
        /// <param name="id">Contact</param>
        /// <returns>Contact entity if such exists, null otherwise</returns>
        IContact GetContactById(int id);
        /// <summary>
        /// Get contacts by the specified email. This method searches among partial matches by email.
        /// </summary>
        /// <param name="email">Partially specified email</param>
        /// <returns></returns>
        IEnumerable<IContact> GetContactsByEmail(string email);
        /// <summary>
        /// Get contacts by the specified phone number. This method searches among partial matches by phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        IEnumerable<IContact> GetContactsByPhoneNumber(string phoneNumber);
        /// <summary>
        /// Get all contacts
        /// </summary>
        /// <returns>Contacts</returns>
        IEnumerable<IContact> GetAllContacts();
    }
}
