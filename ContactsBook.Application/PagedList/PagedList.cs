using System;
using System.Collections.Generic;
using ContactsBook.Application.Interfaces.PagedList;

namespace ContactsBook.Application.PagedList
{
    [Serializable]
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList()
        {
        }

        public PagedList(IEnumerable<T> source, ILimitationParameters limitationParameters, int totalCount)
        {
            PageIndex = limitationParameters.PageIndex;

            TotalCount = totalCount;
            TotalPages = TotalCount / limitationParameters.PageSize;

            if (TotalCount % limitationParameters.PageSize > 0)
                TotalPages++;

            PageSize = limitationParameters.PageSize;

            Items = new List<T>(source);
        }

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage => PageIndex + 1 < TotalPages;
        public bool HasPreviousPage => PageIndex > 0;
        public bool IsPageExists => PageIndex < TotalPages;
        public IList<T> Items { get; private set; }
    }
}