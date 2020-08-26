using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DAL.Entity;
using Application.DAL.Repository;

namespace Application.BLL.Services
{
    public class ContactsService: IContactsService
    {
        public IContactRepository ContactRepository { get; }

        public ContactsService(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }
        public async Task<int> AddContactAsync(IContact contact)
        {
            return await ContactRepository.AddContactAsync(contact);
        }

        public async Task<bool> RemoveContactByIdAsync(int id)
        {
            return await ContactRepository.RemoveContactByIdAsync(id);
        }

        public async Task<bool> UpdateContactAsync(int id, IContact contact)
        {
            return await ContactRepository.UpdateContactAsync(id, contact);
        }

        public async Task<IContact> GetContactByIdAsync(int id)
        {
            return await ContactRepository.GetContactByIdAsync(id);
        }

        public async Task<IEnumerable<IContact>> GetContactsByEmailAsync(string email)
        {
            return await ContactRepository.GetContactsByEmailAsync(email);
        }

        public async Task<IEnumerable<IContact>> GetContactsByPhoneNumberAsync(string phoneNumber)
        {
            return await ContactRepository.GetContactsByPhoneNumberAsync(phoneNumber);
        }

        public async Task<IEnumerable<IContact>> GetAllContactsAsync()
        {
            return await ContactRepository.GetAllContactsAsync();
        }

        public async Task<IEnumerable<IContact>> GetContactsByNameAsync(string name)
        {
            return await ContactRepository.GetContactsByNameAsync(name);
        }
    }
}
