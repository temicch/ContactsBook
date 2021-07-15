using System;

namespace ContactsBook.Application.Interfaces.Models
{
    public record ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}