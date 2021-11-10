using System.Reflection;
using ContactsBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactsBook.DataAccess.MsSql;

public class ContactsDbContext : DbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
