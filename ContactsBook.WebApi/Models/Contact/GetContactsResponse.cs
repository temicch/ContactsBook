using ContactsBook.Application;
using ContactsBook.Application.Models;

namespace ContactsBook.WebApi.Models.Contact
{
    public class GetContactsResponse
    {
        public PagedList<ContactDto> Response { get; set; }
    }
}
