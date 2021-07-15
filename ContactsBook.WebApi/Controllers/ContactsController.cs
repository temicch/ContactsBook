using System;
using System.Threading.Tasks;
using AutoMapper;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Application.Interfaces.PagedList;
using ContactsBook.Application.Interfaces.Services;
using ContactsBook.Application.PagedList;
using ContactsBook.WebApi.Models.Contact;
using Microsoft.AspNetCore.Mvc;

namespace ContactsBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        private readonly IMapper _mapper;

        public ContactsController(IContactsService contactsService, IMapper mapper)
        {
            _contactsService = contactsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<GetContactsResponse>> Get([FromQuery] GetContactsRequest request)
        {
            IPagedList<ContactDto> contacts;
            var limitParameters = new LimitationParameters(request.PageSize, request.PageIndex);

            if (!string.IsNullOrEmpty(request.PhoneNumber?.Trim()))
                contacts = await _contactsService.FindContactsByPhoneNumberAsync(request.PhoneNumber, limitParameters);
            else if (!string.IsNullOrEmpty(request.Name?.Trim()))
                contacts = await _contactsService.FindContactsByNameAsync(request.Name, limitParameters);
            else contacts = await _contactsService.GetContactsAsync(limitParameters);

            var mapping = _mapper.Map<GetContactsResponse>(contacts);

            return new JsonResult(mapping);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetContactResponse>> Get(Guid id)
        {
            var contact = await _contactsService.GetContactByIdAsync(id);

            if (contact == null)
                return NotFound();

            var mapping = _mapper.Map<GetContactResponse>(contact);

            return Ok(mapping);
        }

        [HttpPost]
        public async Task<ActionResult<CreateContactResponse>> Post(CreateContactRequest value)
        {
            var mapping = _mapper.Map<ContactDto>(value);

            var result = await _contactsService.AddContactAsync(mapping);

            if (result == default)
                return BadRequest();

            var mapped = new CreateContactResponse {Id = result};

            return Ok(mapped);
        }

        [HttpPut("{id}")]
        public async Task<StatusCodeResult> Put(Guid id, UpdateContactRequest request)
        {
            if (id != request.Id) return BadRequest();

            var mapped = _mapper.Map<ContactDto>(request);

            var result = await _contactsService.UpdateContactAsync(mapped);

            if (result == false)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> Delete(Guid id)
        {
            var result = await _contactsService.RemoveContactByIdAsync(id);

            if (result == false)
                return BadRequest();

            return NoContent();
        }
    }
}