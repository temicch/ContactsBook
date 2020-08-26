using System.Collections.Generic;

namespace Application.DAL.Entity
{
    public class Contact: IContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<string> PhoneNumbers { get; set; }
        public Contact()
        {
            PhoneNumbers = new HashSet<string>();
        }
    }
}