using System.Collections.Generic;

namespace ContactsBook.Infrastructure.Interfaces
{
    /// <summary>
    ///     Fake data generator for fast seed the data
    /// </summary>
    /// <typeparam name="TEntity">Entity which need fake</typeparam>
    public interface IFakeDataGenerator<TEntity>
    {
        /// <summary>
        ///     Generate faked entities list
        /// </summary>
        /// <param name="count">Count of generated entities</param>
        /// <returns>Generated data list</returns>
        IEnumerable<TEntity> Generate(int count);
    }
}