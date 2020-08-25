using Application.DAL.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Application.DAL.Repository
{
    public class ContactRepository : IContactRepository
    {
        public IMapper Mapper { get; }

        private readonly SqlConnection sqlConnection;
        public ContactRepository(string connectionString,
            IMapper mapper)
        {
            Mapper = mapper;
            sqlConnection = new SqlConnection(connectionString);
        }
        public int AddContact(IContact contact)
        {
            int contactId = 0;
            using (var transact = sqlConnection.BeginTransaction())
            {
                sqlConnection.Open();

                using (var command = sqlConnection.CreateCommand())
                {
                    command.Transaction = transact;
                    command.CommandText = "INSERT INTO Contacts (Name, Email) output INSERTED.ID VALUES (@name, @email)";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@name", SqlDbType.NVarChar);
                    command.Parameters.Add("@email", SqlDbType.NVarChar);
                    command.Parameters[0].Value = contact.Name;
                    command.Parameters[1].Value = contact.Email;

                    contactId = Convert.ToInt32(command.ExecuteScalar());
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

                            command.ExecuteNonQuery();
                        }
                    }
                try
                {
                    transact.Commit();
                }
                catch (Exception e)
                {
                    transact.Rollback();
                }
                sqlConnection.Close();
            }

            return contactId;
        }

        public bool RemoveContactById(int id)
        {
            return true;

            sqlConnection.Open();

            bool isRemoved = false;

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Contacts WHERE ID = @id";
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters[0].Value = id;

                isRemoved = command.ExecuteNonQuery() > 0;
            }
            sqlConnection.Close();
            return isRemoved;
        }

        public bool UpdateContact(IContact contact)
        {
            return true;
        }

        public IContact GetContactById(int id)
        {
            return StubContacts.FirstOrDefault();

            Contact contact = null;
            sqlConnection.Open();

            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Contacts WHERE Id = @id";
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters[0].Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        contact = Mapper.Map<DbDataReader, Contact>(reader);
                    }
                }
            }

            if (contact != null)
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM PhoneNumbers WHERE Contact_Id = @id";
                    command.Parameters.Add("@id", SqlDbType.Int);
                    command.Parameters[0].Value = id;
                    using (var reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            contact.PhoneNumbers.Add(Convert.ToString(reader["Number"]));
                        }
                    }
                }
            }
            sqlConnection.Close();
            return contact;
        }

        public IEnumerable<IContact> GetContactsByEmail(string email)
        {
            return StubContacts;
        }

        public IEnumerable<IContact> GetContactsByPhoneNumber(string phoneNumber)
        {
            return StubContacts;
        }

        public IEnumerable<IContact> GetAllContacts()
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