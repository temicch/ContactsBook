using AutoMapper;
using AutoMapper.Data.Configuration.Conventions;
using ContactsBook.Domain.Entities;
using ContactsBook.Domain.ValueObjects;
using System.Data;

namespace ContactsBook.DataAccess.MsSql
{
    public class DataAccessMapping : Profile
    {
        public DataAccessMapping()
        {
            CreateMap<IDataRecord, Contact>()
                .ForPath(dest => dest.Email, src => src.MapFrom(src => new Email(src["Email"].ToString())))
                .ForPath(dest => dest.PhoneNumber, src => src.MapFrom(src => new PhoneNumber((long)src["PhoneNumber"])));

            AddMemberConfiguration()
                .AddMember<DataRecordMemberConfiguration>();
        }
    }
}
