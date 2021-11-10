using System.Collections.Generic;
using ContactsBook.Infrastructure.Interfaces.SelectResult;

namespace ContactsBook.DataAccess.MsSql.SelectResult;

public class SelectResult<T> : ISelectResult<T>
{
    public SelectResult(List<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
}
