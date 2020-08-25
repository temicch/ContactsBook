using System.Collections.Generic;

namespace Application.DAL.Entity
{
    public interface IContact
    {
        int Id { get; }
        string Name { get; }
        string Email { get; }
        ICollection<string> PhoneNumbers { get; }
    }
}