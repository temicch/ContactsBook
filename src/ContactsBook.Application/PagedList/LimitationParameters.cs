using System;
using ContactsBook.Application.Interfaces.PagedList;

namespace ContactsBook.Application.PagedList;

public record struct LimitationParameters : ILimitationParameters
{
    public LimitationParameters(int pageSize = 20, int pageIndex = 0)
    {
        if (pageSize < 1)
            throw new ArgumentException("Page size at least must be 1", nameof(pageSize));
        if (pageIndex < 0)
            throw new ArgumentException("Page index must be non negative value", nameof(pageIndex));

        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public LimitationParameters(): this(20, 0)
    {

    }

    public int PageIndex { get; init; } = 0;
    public int PageSize { get; init; } = 20;
}
