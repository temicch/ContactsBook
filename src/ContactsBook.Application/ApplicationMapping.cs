using AutoMapper;
using ContactsBook.Application.Interfaces.Models;
using ContactsBook.Domain.Entities;
using ContactsBook.Domain.ValueObjects;

namespace ContactsBook.Application
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber.Value));

            CreateMap<ContactDto, Contact>()
                .ForMember(dest => dest.Email, src => src.MapFrom(src => new Email(src.Email)))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => new PhoneNumber(src.PhoneNumber)));
        }
    }
}