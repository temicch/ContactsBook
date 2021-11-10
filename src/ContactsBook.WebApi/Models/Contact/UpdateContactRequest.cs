using System;
using ContactsBook.Application.Interfaces.Services;
using ContactsBook.WebApi.Extensions;
using FluentValidation;

namespace ContactsBook.WebApi.Models.Contact;

public class UpdateContactRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long PhoneNumber { get; set; }
    public string Email { get; set; }
}

public class UpdateContactRequestValidator : AbstractValidator<UpdateContactRequest>
{
    public UpdateContactRequestValidator(IContactsService contactsService)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                var contact = await contactsService.GetContactByIdAsync(id);
                return contact != null;
            })
            .WithMessage("Contact with id '{PropertyValue}' is not exists");

        RuleFor(x => x.Name)
            .CbName();

        RuleFor(x => x.Email)
            .CbEmailAddress();

        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .CbPhoneNumber()
            .MustAsync(async (request, phoneNumber, cancellationToken) =>
            {
                var contact = await contactsService.GetContactByPhoneNumberAsync(phoneNumber.ToString());
                return contact == null || contact.Id == request.Id;
            })
            .WithMessage("Contact with phone number '{PropertyValue}' is already exists");
    }
}
