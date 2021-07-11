using Xunit;

namespace ContactsBook.Application.UnitTests.Fixture.SourcePagedList
{
    /// <summary>
    ///     Setup for Count of elements for collection, page size, page index,
    ///     total count of elements in collection, and for assertion parameters.
    ///     Has next page, has previous page, and total count of pages.
    /// </summary>
    public class ValidSourcePagedListSetup : TheoryData<int, int, int, int, bool, bool, int>
    {
        public ValidSourcePagedListSetup()
        {
            Add(100, 20, 0, 100, true, false, 5);
            Add(100, 20, 1, 100, true, true, 5);
            Add(100, 20, 16, 100, false, true, 5);
            Add(100, 3, 5, 100, true, true, 34);
        }
    }
}