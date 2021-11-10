namespace ContactsBook.Application.Interfaces.PagedList;

public interface ILimitationParameters
{
    /// <summary>
    ///     Page index
    /// </summary>
    int PageIndex { get; init; }

    /// <summary>
    ///     Page size
    /// </summary>
    int PageSize { get; init; }
}
