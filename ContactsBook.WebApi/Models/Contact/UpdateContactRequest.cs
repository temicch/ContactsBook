using System;
using ContactsBook.WebApi.Extensions;
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
                .CbName();

            RuleFor(x => x.Email)
                .CbEmailAddress();

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .CbPhoneNumber();
        }
    }
}