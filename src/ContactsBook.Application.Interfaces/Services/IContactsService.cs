using System;
using System.Threading.Tasks;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Application.Interfaces.PagedList;

namespace ContactsBook.Application.Interfaces.Services;

public interface IContactsService
{
    /// <summary>
    ///     Add new contact
    /// </summary>
    /// <param name="contact">Contact</param>
    /// <returns>Id of added entity</returns>
    Task<Guid> AddContactAsync(ContactDto contact);

    /// <summary>
    ///     Remove contact by id
    /// </summary>
    /// <param name="id">Contact id</param>
    /// <returns>True if success, false otherwise</returns>
    Task<bool> RemoveContactByIdAsync(Guid id);

    /// <summary>
    ///     Update contact
    /// </summary>
    /// <param name="contact">Contact</param>
    /// <returns>True if success, false otherwise</returns>
    Task<bool> UpdateContactAsync(ContactDto contact);

    /// <summary>
    ///     Get contact by id
    /// </summary>
    /// <param name="id">Contact id</param>
    /// <returns>Contact entity if such exists, null otherwise</returns>
    Task<ContactDto> GetContactByIdAsync(Guid id);

    /// <summary>
    ///     Get contact by phone number
    /// </summary>
    /// <param name="phoneNumber">Contact phone number</param>
    /// <returns>Contact entity if such exists, null otherwise</returns>
    Task<ContactDto> GetContactByPhoneNumberAsync(string phoneNumber);

    /// <summary>
    ///     Get contacts
    /// </summary>
    /// <returns>Paginated result with <seealso cref="ContactDto" /> items</returns>
    Task<IPagedList<ContactDto>> GetContactsAsync(ILimitationParameters limitationParameters);

    /// <summary>
    ///     Get contacts by the specified phone number. This method searches among partial matches by phone number.
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns>Paginated result with <seealso cref="ContactDto" /> items</returns>
    Task<IPagedList<ContactDto>> FindContactsByPhoneNumberAsync(string phoneNumber,
        ILimitationParameters limitationParameters);

    /// <summary>
    ///     Get contacts by the specified person name. This method searches among partial matches by person name.
    /// </summary>
    /// <param name="name">Person name</param>
    /// <returns>Paginated result with <seealso cref="ContactDto" /> items</returns>
    Task<IPagedList<ContactDto>> FindContactsByNameAsync(string name, ILimitationParameters limitationParameters);

    /// <summary>
    ///     Check for unique phone number
    /// </summary>
    /// <param name="phoneNumber">Phone number</param>
    /// <returns>True if such contact exists with phone number, false otherwise</returns>
    Task<bool> IsPhoneNumberExistsAsync(string phoneNumber);
}
