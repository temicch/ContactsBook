using System.Collections.Generic;

namespace ContactsBook.Application.Interfaces.PagedList
{
    /// <summary>
    ///     Paged list interface
    /// </summary>
    public interface IPagedList<T>
    {
        /// <summary>
        ///     Page index
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        ///     Page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        ///     Total count
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        ///     Total pages
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        ///     Has previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        ///     Has next page
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        ///     Is current page exists. False will returned if page is more than max page index
        /// </summary>
        bool IsPageExists { get; }

        /// <summary>
        ///     Collection
        /// </summary>
        IList<T> Items { get; }
    }
}