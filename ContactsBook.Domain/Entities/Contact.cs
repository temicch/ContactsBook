using ContactsBook.Domain.ValueObjects;

namespace ContactsBook.Domain.Entities
{
    public class Contact: BaseEntity
    {
        public string Name { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Email Email { get; set; }
    }
}