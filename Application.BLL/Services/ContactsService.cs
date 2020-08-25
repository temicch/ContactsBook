using System.Collections.Generic;
using Application.DAL.Entity;
using Application.DAL.Repository;

namespace Application.BLL.Services
{
    class ContactsService: IContactsService
    {
        public IContactRepository ContactRepository { get; }

        public ContactsService(IContactRepository contactRepository)
        {
            ContactRepository = contactRepository;
        }
        public int AddContact(IContact contact)
        {
            return ContactRepository.AddContact(contact);
        }

        public bool RemoveContactById(int id)
        {
            return ContactRepository.RemoveContactById(id);
        }

        public bool UpdateContact(IContact contact)
        {
            return ContactRepository.UpdateContact(contact);
        }

        public IContact GetContactById(int id)
        {
            return ContactRepository.GetContactById(id);
        }

        public IEnumerable<IContact> GetContactsByEmail(string email)
        {
            return ContactRepository.GetContactsByEmail(email);
        }

        public IEnumerable<IContact> GetContactsByPhoneNumber(string phoneNumber)
        {
            return ContactRepository.GetContactsByPhoneNumber(phoneNumber);
        }

        public IEnumerable<IContact> GetAllContacts()
        {
            return ContactRepository.GetAllContacts();
        }
    }
}
