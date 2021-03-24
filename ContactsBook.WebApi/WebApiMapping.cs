using AutoMapper;
using ContactsBook.Application;
using ContactsBook.Application.Models;
using ContactsBook.WebApi.Models.Contact;

namespace ContactsBook.WebApi
{
    public class WebApiMapping : Profile
    {
        public WebApiMapping()
        {
            CreateMap<CreateContactRequest, ContactDto>();
            CreateMap<UpdateContactRequest, ContactDto>();
            CreateMap<ContactDto, GetContactResponse>()
                .ForMember(dest => dest.Response, src => src.MapFrom(src => src));
            CreateMap<PagedList<ContactDto>, GetContactsResponse>()
                .ForMember(dest => dest.Response, src => src.MapFrom(src => src)); 
        }
    }
}
