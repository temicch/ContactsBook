using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ContactsBook.Application;
using ContactsBook.Domain.Entities;

namespace ContactsBook.DataAccess.MsSql.Repository.Helpers
{
    internal static class ContactRepositoryHelpers
    {
        public static async Task<Contact> SelectContactAsync(SqlConnection sqlConnection, IMapper mapper, Guid id)
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "select * from Contacts where Id = @id";
                command.Parameters.AddWithValue("@id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                        return null;

                    await reader.ReadAsync();
                    var contact = mapper.Map<DbDataReader, Contact>(reader);

                    return contact;
                }
            }
        }
        public static async Task<Guid> UpdateContactAsync(SqlConnection sqlConnection, Contact contact)
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText =
                    "UPDATE Contacts SET Name = @name, Email = @email, PhoneNumber = @phoneNumber where Id = @id";
                command.Parameters.AddWithValue("@id", contact.Id);
                command.Parameters.AddWithValue("@name", contact.Name);
                command.Parameters.AddWithValue("@email", contact.Email.Value);
                command.Parameters.AddWithValue("@phoneNumber", contact.PhoneNumber.Value);

                return await command.ExecuteNonQueryAsync() > 0 ? contact.Id : default;
            }
        }
        public static async Task<Guid> InsertContactAsync(SqlConnection sqlConnection, Contact contact)
        {
            Guid contactId;
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText =
                    "INSERT INTO Contacts (Name, Email, PhoneNumber) output INSERTED.ID VALUES (@name, @email, @phoneNumber)";
                command.Parameters.AddWithValue("@name", contact.Name);
                command.Parameters.AddWithValue("@email", contact.Email.Value ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@phoneNumber", contact.PhoneNumber.Value);

                contactId = (Guid) await command.ExecuteScalarAsync();
            }

            return contactId;
        }

        public static async Task InsertContactsAsync(string connectionString, string tableName, IEnumerable<Contact> contacts)
        {
            using (var copy = new SqlBulkCopy(connectionString))
            {
                copy.DestinationTableName = tableName;

                copy.ColumnMappings.Add(nameof(Contact.Id), nameof(Contact.Id));
                copy.ColumnMappings.Add(nameof(Contact.Name), nameof(Contact.Name));
                copy.ColumnMappings.Add(nameof(Contact.PhoneNumber), nameof(Contact.PhoneNumber));
                copy.ColumnMappings.Add(nameof(Contact.Email), nameof(Contact.Email));

                await copy.WriteToServerAsync(ToDataTable(contacts));
            }
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static async Task<SelectResult<Contact>> SelectAllContactsAsync(SqlConnection sqlConnection, IMapper mapper, LimitationParameters limitationParameters)
        {
            return await SelectWithParameters(sqlConnection, mapper, "*", "", limitationParameters.GetMSSqlAddition());
        }

        public static async Task<SelectResult<Contact>> SelectNameContactsAsync(SqlConnection sqlConnection,
            IMapper mapper,
            string name,
            LimitationParameters limitationParameters)
        {
            return await SelectWithParameters(sqlConnection, mapper, "*", "where Name like @name", limitationParameters.GetMSSqlAddition(), new[] { new SqlParameter("@name", $"%{name}%") });
        }
        public static async Task<SelectResult<Contact>> SelectPhoneContactsAsync(SqlConnection sqlConnection, IMapper mapper, string phoneNumber, LimitationParameters limitationParameters)
        {
            return await SelectWithParameters(sqlConnection, mapper, "*", "where PhoneNumber like @phoneNumber", limitationParameters.GetMSSqlAddition(), new[] { new SqlParameter("@phoneNumber", $"%{phoneNumber}%") });
        }
        public static async Task<bool> RemoveContactAsync(SqlConnection sqlConnection, Guid id)
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Contacts WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        private static async Task<SelectResult<Contact>> SelectWithParameters(
            SqlConnection sqlConnection, 
            IMapper mapper,
            string selectColumn, 
            string grouping, 
            string filters, 
            SqlParameter[] parameters = null)
        {
            int totalCount;
            totalCount = await CountOfSelectQuery(sqlConnection, grouping, parameters);

            return await SelectQueryInvoke(sqlConnection, mapper, selectColumn, grouping, filters, parameters, totalCount);
        }

        private static async Task<SelectResult<Contact>> SelectQueryInvoke(SqlConnection sqlConnection, IMapper mapper, string selectColumn, string grouping, string filters, SqlParameter[] parameters, int totalCount)
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = $"select {selectColumn} from Contacts {grouping} {filters}";

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                        return new(null, totalCount);

                    var result = new List<Contact>();
                    while (await reader.ReadAsync())
                    {
                        var contact = mapper.Map<DbDataReader, Contact>(reader);
                        result.Add(contact);
                    }

                    return new(result, totalCount);
                }
            }
        }

        private static async Task<int> CountOfSelectQuery(SqlConnection sqlConnection, string filters, SqlParameter[] parameters)
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = $"select count(*) from Contacts {filters}";
                if(parameters != null)
                    command.Parameters.AddRange(parameters);

                var result = (int) await command.ExecuteScalarAsync();

                command.Parameters.Clear();

                return result;
            }
        }
    }
}
