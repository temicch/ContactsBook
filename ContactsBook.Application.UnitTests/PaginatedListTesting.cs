using System;
using System.Linq;
using ContactsBook.Application.PagedList;
using ContactsBook.Application.UnitTests.Fixture.SourcePagedList;
using Xunit;

namespace ContactsBook.Application.UnitTests
{
    public class PaginatedListTesting
    {
        [Theory]
        [ClassData(typeof(ValidSourcePagedListSetup))]
        public void CreatedPagedList_ShouldHave_CorrectProperties(int countOfElements,
            int pageSize,
            int pageIndex,
            int totalCount,
            bool hasNextPage,
            bool hasPrevPage,
            int totalPage)
        {
            // Assign
            var stringCollection = Enumerable.Range(0, countOfElements).Select(i => i.ToString()).ToList();
            var limitationParameters = new LimitationParameters(pageSize, pageIndex);

            // Act
            var pagedList = new PagedList<string>(stringCollection, limitationParameters, totalCount);

            // Assert
            Assert.Equal(hasNextPage, pagedList.HasNextPage);
            Assert.Equal(hasPrevPage, pagedList.HasPreviousPage);
            Assert.Equal(totalPage, pagedList.TotalPages);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void CreatePagedList_MustThrow_OnNonPositiveInputParameters(int totalCount)
        {
            // Assign
            var limitationParameters = new LimitationParameters(50, 1);

            // Act
            Action pagedListCreator = () => new PagedList<string>(null, limitationParameters, totalCount);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(pagedListCreator);
        }

        [Theory]
        [InlineData(100, 99, 1, true)]
        [InlineData(100, 100, 1, false)]
        [InlineData(100, 100, 10, false)]
        [InlineData(100, 0, 10, true)]
        [InlineData(1, 0, 10, true)]
        [InlineData(1, 1, 10, false)]
        public void CreatedPagedList_MustCorrectlyReturnsIsPageExists(int totalCount, int pageIndex, int pageSize,
            bool isPageExists)
        {
            // Assign
            var limitationParameters = new LimitationParameters(pageSize, pageIndex);

            // Act
            var pagedList = new PagedList<string>(null, limitationParameters, totalCount);

            // Assert
            Assert.Equal(isPageExists, pagedList.IsPageExists);
        }
    }
}