using System.Collections.Generic;

namespace ContactsBook.Application
{
    public class SelectResult<T>
    {
        public SelectResult(List<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public List<T> Items { get; set; }
        public int TotalCount{ get; set; }
    }
}