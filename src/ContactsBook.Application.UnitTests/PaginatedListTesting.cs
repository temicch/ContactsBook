using System;
using System.Linq;
using ContactsBook.Application.PagedList;
using Xunit;

namespace ContactsBook.Application.UnitTests
{
    public class PaginatedListTesting
    {
        [Theory]
        [InlineData(100, 20, 0, 100, true, false, 5)]
        [InlineData(100, 20, 1, 100, true, true, 5)]
        [InlineData(100, 20, 16, 100, false, true, 5)]
        [InlineData(100, 3, 5, 100, true, true, 34)]
        [InlineData(0, 10, 0, 0, false, false, 0)]
        public void Created_PagedList_Is_Have_CorrectProperties(int countOfElements,
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
        [InlineData(-1)]
        [InlineData(-1000)]
        public void PagedList_With_Negative_TotalCount_Cant_Be_Created(int totalCount)
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
        [InlineData(0, 0, 10, false)]
        public void Created_PagedList_Returns_Correct_Flag_Of_Existing_Page(int totalCount, int pageIndex, int pageSize,
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