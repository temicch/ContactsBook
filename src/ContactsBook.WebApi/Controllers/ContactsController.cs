using System;
using System.Threading.Tasks;
using AutoMapper;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Application.Interfaces.PagedList;
using ContactsBook.Application.Interfaces.Services;
using ContactsBook.Application.PagedList;
using ContactsBook.WebApi.Models.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactsBook.WebApi.Controllers;

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

    /// <summary>
    ///     Retrieves contacts
    /// </summary>
    /// <remarks>
    ///     This method may use for search contacts by specified name or phone number.
    ///     If valid phone number specified, then search will be evaluated for him. Search by name
    ///     will be evaluated otherwise (if such specified)
    /// </remarks>
    /// <param name="request">Paginated request</param>
    /// <response code="200">Paged list with contacts</response>
    /// <response code="400">Something went wrong</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetContactsResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] GetContactsRequest request)
    {
        IPagedList<ContactDto> contacts;
        var limitParameters = new LimitationParameters(request.PageSize, request.PageIndex);

        if (!string.IsNullOrEmpty(request.PhoneNumber?.Trim()))
            contacts = await _contactsService.FindContactsByPhoneNumberAsync(request.PhoneNumber, limitParameters);
        else if (!string.IsNullOrEmpty(request.Name?.Trim()))
            contacts = await _contactsService.FindContactsByNameAsync(request.Name, limitParameters);
        else
            contacts = await _contactsService.GetContactsAsync(limitParameters);

        var mapping = _mapper.Map<GetContactsResponse>(contacts);

        return new JsonResult(mapping);
    }

    /// <summary>
    ///     Retrieves a specific contact by unique id
    /// </summary>
    /// <param name="id">Contact id</param>
    /// <response code="200">Finded contact</response>
    /// <response code="404">Contact with specified id is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var contact = await _contactsService.GetContactByIdAsync(id);

        if (contact == null)
            return NotFound();

        var mapping = _mapper.Map<GetContactResponse>(contact);

        return Ok(mapping);
    }

    /// <summary>
    ///     Create contact
    /// </summary>
    /// <param name="value">Contact</param>
    /// <response code="200">Contact was successfully created with received id</response>
    /// <response code="400">Something went wrong</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateContactResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CreateContactRequest value)
    {
        var mapping = _mapper.Map<ContactDto>(value);

        var result = await _contactsService.AddContactAsync(mapping);

        if (result == default)
            return BadRequest();

        var mapped = new CreateContactResponse { Id = result };

        return Ok(mapped);
    }

    /// <summary>
    ///     Update contact
    /// </summary>
    /// <param name="id">Contact id</param>
    /// <param name="request">Contact's new values</param>
    /// <response code="200">Contact was successfully updated</response>
    /// <response code="400">Something went wrong</response>
    /// <response code="404">Contact with specified id is not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateContactRequest request)
    {
        if (id != request.Id) return BadRequest();

        var mapped = _mapper.Map<ContactDto>(request);

        var result = await _contactsService.UpdateContactAsync(mapped);

        if (result == false)
            return BadRequest();

        return Ok();
    }

    /// <summary>
    ///     Remove contact by specified Id
    /// </summary>
    /// <param name="id">Contact id</param>
    /// <response code="204">Contact was successfully removed</response>
    /// <response code="400">Something went wrong</response>
    /// <response code="404">Contact with specified id is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (await _contactsService.GetContactByIdAsync(id) == null)
            return NotFound();

        var result = await _contactsService.RemoveContactByIdAsync(id);

        if (result == false)
            return BadRequest();

        return NoContent();
    }

    public Task Del()
    {
        return Task.CompletedTask;
    }
}
