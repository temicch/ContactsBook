using ContactsBook.Application.Interfaces.PagedList;

namespace ContactsBook.DataAccess.MsSql.Extensions;

internal static class LimitationParametersExtensions
{
    /// <summary>
    ///     Additional part of query for select with limitation parameters
    /// </summary>
    /// <returns></returns>
    public static string GetMSSqlAddition(this ILimitationParameters parameters)
    {
        var queryString =
            $"order by Name {(parameters.PageSize > 0 ? $"offset {parameters.PageIndex * parameters.PageSize} rows fetch next {parameters.PageSize} rows only" : "")}";

        return queryString;
    }
}
