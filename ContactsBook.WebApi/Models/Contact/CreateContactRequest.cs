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
        public CreateContactRequstValidator()
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
