namespace ContactsBook.Application
{
    public record LimitationParameters(int PageSize = 20, int PageIndex = 0)
    {
        public string GetMSSqlAddition()
        {
            var queryString = $"order by Name {(PageSize > 0 ? $"offset {(PageIndex) * PageSize} rows fetch next {PageSize} rows only" : "")}";

            return queryString;
        }
    }
}
