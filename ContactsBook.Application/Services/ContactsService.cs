using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Application.Interfaces.PagedList;
using ContactsBook.Application.Interfaces.Services;
using ContactsBook.Application.PagedList;
using ContactsBook.Domain.Entities;
using ContactsBook.Infrastructure.Interfaces.Repository;

namespace ContactsBook.Application.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactRepository<Contact> _contactRepository;
        private readonly IMapper _mapper;

        public ContactsService(IContactRepository<Contact> contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddContactAsync(ContactDto contact)
        {
            return await _contactRepository.InsertAsync(_mapper.Map<Contact>(contact));
        }

        public async Task<bool> RemoveContactByIdAsync(Guid id)
        {
            return await _contactRepository.DeleteByIdAsync(id);
        }

        public async Task<bool> UpdateContactAsync(ContactDto contact)
        {
            return await _contactRepository.UpdateAsync(_mapper.Map<Contact>(contact));
        }

        public async Task<ContactDto> GetContactByIdAsync(Guid id)
        {
            return _mapper.Map<ContactDto>(await _contactRepository.GetByIdAsync(id));
        }

        public async Task<IPagedList<ContactDto>> FindContactsByPhoneNumberAsync(string phoneNumber,
            ILimitationParameters limitationParameters)
        {
            var result = await _contactRepository.FindByPhoneNumberAsync(phoneNumber, limitationParameters);

            var paginatedList = new PagedList<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items),
                limitationParameters, result.TotalCount);

            return paginatedList;
        }

        public async Task<IPagedList<ContactDto>> GetContactsAsync(ILimitationParameters limitationParameters)
        {
            var result = await _contactRepository.GetAllAsync(limitationParameters);

            var paginatedList = new PagedList<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items),
                limitationParameters, result.TotalCount);

            return paginatedList;
        }

        public async Task<IPagedList<ContactDto>> FindContactsByNameAsync(string name,
            ILimitationParameters limitationParameters)
        {
            var result = await _contactRepository.FindByNameAsync(name, limitationParameters);

            var paginatedList = new PagedList<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items),
                limitationParameters, result.TotalCount);

            return paginatedList;
        }

        public async Task<bool> IsPhoneNumberExistsAsync(string phoneNumber)
        {
            return await _contactRepository.IsPhoneNumberExistsAsync(phoneNumber);
        }

        public async Task<IPagedList<ContactDto>> GetContactsAsync(ILimitationParameters limitationParameters,
            string phoneNumber = null, string name = null)
        {
            if (phoneNumber != null)
                return await FindContactsByPhoneNumberAsync(phoneNumber, limitationParameters);

            if (name != null)
                return await FindContactsByNameAsync(name, limitationParameters);

            return await GetContactsAsync(limitationParameters);
        }

        public async Task<ContactDto> GetContactByPhoneNumberAsync(string phoneNumber)
        {
            return _mapper.Map<ContactDto>(await _contactRepository.GetByPhoneNumberAsync(phoneNumber));
        }
    }
}