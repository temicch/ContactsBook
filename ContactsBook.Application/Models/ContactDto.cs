﻿using System;

namespace ContactsBook.Application.Models
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
