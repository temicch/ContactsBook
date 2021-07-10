using ContactsBook.Application.Interfaces.Services;
using ContactsBook.WebApi.Extensions;
using FluentValidation;

namespace ContactsBook.WebApi.Models.Contact
{
    public class CreateContactRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
    }

    public class CreateContactRequstValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactRequstValidator(IContactsService contactsService)
        {
            RuleFor(x => x.Name)
                .CbName();

            RuleFor(x => x.Email)
                .CbEmailAddress();

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .CbPhoneNumber()
                .MustAsync(async (phoneNumber, cancellationToken) =>
                    !await contactsService.IsPhoneNumberExistsAsync(phoneNumber.ToString()))
                    .WithMessage("Contact with phone number '{PropertyValue}' is already exists");
        }
    }
}