using System.Linq;
using FluentValidation;

namespace ContactsBook.WebApi.Models.Contact
{
    public class GetContactsRequest
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public int PageSize { get; set; } = 20;
        public int PageIndex { get; set; } = 0;
    }

    public class GetContactsRequestValidator : AbstractValidator<GetContactsRequest>
    {
        public GetContactsRequestValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(50);
            RuleFor(x => x.Name)
                .MinimumLength(3);
            RuleFor(x => x.PhoneNumber)
                .MinimumLength(3)
                .Must(x => x?.ToCharArray().All(y => char.IsDigit(y)) ?? true)
                .WithMessage("Phone number must contain only digits");
        }
    }
}