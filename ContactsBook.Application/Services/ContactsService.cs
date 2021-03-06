﻿using AutoMapper;
using ContactsBook.Application.Models;
using ContactsBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<PagedList<ContactDto>> GetContactsByPhoneNumberAsync(string phoneNumber, LimitationParameters limitationParameters)
        {
            var result = await _contactRepository.GetByPhoneNumberAsync(phoneNumber, limitationParameters);

            var paginatedList = new PagedList<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items), limitationParameters, result.TotalCount);

            return paginatedList;
        }

        public async Task<PagedList<ContactDto>> GetContactsAsync(LimitationParameters limitationParameters)
        {
            var result = await _contactRepository.GetAsync(limitationParameters);

            var paginatedList = new PagedList<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items), limitationParameters, result.TotalCount);

            return paginatedList;
        }

        public async Task<PagedList<ContactDto>> GetContactsByNameAsync(string name, LimitationParameters limitationParameters)
        {
            var result = await _contactRepository.GetByNameAsync(name, limitationParameters);

            var paginatedList = new PagedList<ContactDto>(_mapper.Map<List<ContactDto>>(result.Items), limitationParameters, result.TotalCount);

            return paginatedList;
        }

        public async Task<PagedList<ContactDto>> GetContactsAsync(LimitationParameters limitationParameters, string phoneNumber = null, string name = null)
        {
            if (phoneNumber != null)
                return await GetContactsByPhoneNumberAsync(phoneNumber, limitationParameters);
            else if (name != null)
                return await GetContactsByNameAsync(name, limitationParameters);
            return await GetContactsAsync(limitationParameters);
        }
    }
}
