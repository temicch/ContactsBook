using Application.DAL.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.DAL.Repository
{
    public class ContactRepository : IContactRepository
    {
        public IMapper Mapper { get; }
        public ILogger<ContactRepository> Logger { get; }

        private readonly SqlConnection sqlConnection;
        public ContactRepository(string connectionString,
            IMapper mapper)
        {
            Mapper = mapper;
            sqlConnection = new SqlConnection(connectionString);
        }
        public async Task<int> AddContactAsync(IContact contact)
        {
            int contactId = 0;
            using (var transact = sqlConnection.BeginTransaction())
            {
                await sqlConnection.OpenAsync();

                using (var command = sqlConnection.CreateCommand())
                {
                    command.Transaction = transact;
                    command.CommandText = "INSERT INTO Contacts (Name, Email) output INSERTED.ID VALUES (@name, @email)";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@name", SqlDbType.NVarChar);
                    command.Parameters.Add("@email", SqlDbType.NVarChar);
                    command.Parameters[0].Value = contact.Name;
                    command.Parameters[1].Value = contact.Email;

                    contactId = Convert.ToInt32(await command.ExecuteScalarAsync());
                }
                if(contact.PhoneNumbers.Any())
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.Transaction = transact;
                        command.CommandText = "INSERT INTO PhoneNumbers (Contact_Id, Number) VALUES (@id, @number)";
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("@name", SqlDbType.NVarChar);
                        command.Parameters.Add("@email", SqlDbType.NVarChar);
                        foreach (var number in contact.PhoneNumbers)
                        {
                            command.Parameters[0].Value = contact.Id;
                            command.Parameters[1].Value = number;

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                try
                {
                    transact.Commit();
                }
                catch (Exception e)
                {
                    Logger.LogWarning(e, e.Message);
                    transact.Rollback();
                }
                sqlConnection.Close();
            }

            return contactId;
        }

        public async Task<bool> RemoveContactByIdAsync(int id)
        {
            return true;

            await sqlConnection.OpenAsync();

            bool isRemoved = false;

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Contacts WHERE ID = @id";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters[0].Value = id;

                isRemoved = await command.ExecuteNonQueryAsync() > 0;
            }
            sqlConnection.Close();
            return isRemoved;
        }

        public async Task<bool> UpdateContactAsync(int id, IContact contact)
        {
            return true;
        }

        public async Task<IContact> GetContactByIdAsync(int id)
        {
            Contact contact = null;
            await sqlConnection.OpenAsync();

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "select t1.Id, t1.Name, t1.Email, t2.Number from Contacts as t1 left join PhoneNumbers as t2 on t1.Id = t2.Contact_Id where t1.Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters[0].Value = id;
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        contact = Mapper.Map<DbDataReader, Contact>(reader);
                        var phoneNumber = reader.IsDBNull(3) ? null : (await reader.GetFieldValueAsync<string>(3)).Trim();
                        if (!string.IsNullOrEmpty(phoneNumber))
                            contact.PhoneNumbers.Add(phoneNumber);
                        while (await reader.ReadAsync())
                        {
                            phoneNumber = reader.IsDBNull(3) ? null : (await reader.GetFieldValueAsync<string>(3)).Trim();
                            if (!string.IsNullOrEmpty(phoneNumber))
                                contact.PhoneNumbers.Add(phoneNumber);
                        }
                    }
                }
            }
            sqlConnection.Close();
            return contact;
        }

        public async Task<IEnumerable<IContact>> GetContactsByEmailAsync(string email)
        {
            return StubContacts;
        }

        public async Task<IEnumerable<IContact>> GetContactsByPhoneNumberAsync(string phoneNumber)
        {
            return StubContacts;
        }

        public async Task<IEnumerable<IContact>> GetAllContactsAsync()
        {
            return StubContacts;
        }

        public async Task<IEnumerable<IContact>> GetContactsByNameAsync(string name)
        {
            return StubContacts;
        }

        private List<Contact> StubContacts = new List<Contact>()
        {
            new Contact()
            {
                Email = "Email1",
                Name = "Name1",
                PhoneNumbers = new List<string>()
                {
                    "1",
                    "2",
                    "3",
                    "4",
                }

            },
            new Contact()
            {
                Email = "Email2",
                Name = "Name2",
                PhoneNumbers = new List<string>()
                {
                    "1",
                    "2",
                    "4",
                }

            },
            new Contact()
            {
                Email = "Email3",
                Name = "Name3",
                PhoneNumbers = new List<string>()
                {
                    "1",
                    "2",
                }

            },
        };
    }
}