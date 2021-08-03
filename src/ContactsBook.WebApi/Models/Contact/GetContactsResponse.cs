using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Application.PagedList;

namespace ContactsBook.WebApi.Models.Contact
{
    public class GetContactsResponse
    {
        public PagedList<ContactDto> Response { get; set; }
    }
}