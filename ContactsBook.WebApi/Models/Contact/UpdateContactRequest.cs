﻿using System;
using FluentValidation;

namespace ContactsBook.WebApi.Models.Contact
{
    public class UpdateContactRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
    {
        public UpdateContactRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(3, 28);
            RuleFor(x => x.Email)
                .EmailAddress();
            RuleFor(x => x.PhoneNumber)
                .InclusiveBetween(10000000000, 99999999999);
        }
    }
}