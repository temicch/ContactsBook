using AutoMapper;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Application.PagedList;
using ContactsBook.WebApi.Models.Contact;

namespace ContactsBook.WebApi;

public class WebApiMapping : Profile
{
    public WebApiMapping()
    {
        CreateMap<CreateContactRequest, ContactDto>()
            .ForMember(dest => dest.Id, src => src.Ignore());
        CreateMap<UpdateContactRequest, ContactDto>();
        CreateMap<ContactDto, GetContactResponse>()
            .ForMember(dest => dest.Response, src => src.MapFrom(src => src));
        CreateMap<PagedList<ContactDto>, GetContactsResponse>()
            .ForMember(dest => dest.Response, src => src.MapFrom(src => src));
    }
}
