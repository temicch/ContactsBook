using System.Collections.Generic;

namespace ContactsBook.Infrastructure.Interfaces.SelectResult;

/// <summary>
///     Result of 'select' query. Contains partly selected data with total count of entities
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISelectResult<T>
{
    List<T> Items { get; set; }
    int TotalCount { get; set; }
}
