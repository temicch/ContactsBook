using ContactsBook.Application.Interfaces.PagedList;

namespace ContactsBook.Application.PagedList
{
    public record LimitationParameters(int PageSize = 20, int PageIndex = 0) : ILimitationParameters;
}