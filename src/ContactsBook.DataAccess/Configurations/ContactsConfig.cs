using ContactsBook.Domain.Entities;
using ContactsBook.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactsBook.DataAccess.MsSql.Configurations
{
    internal class ContactsConfig : IEntityTypeConfiguration<Contact>
    {
        internal const string TABLE_NAME = "Contacts";

        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(TABLE_NAME);

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.PhoneNumber)
                .IsUnique();

            builder.Property(p => p.PhoneNumber)
                .HasConversion(
                    value => value.Value,
                    value => new PhoneNumber(value))
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(p => p.Email)
                .HasConversion(
                    value => value.Value,
                    value => new Email(value))
                .HasMaxLength(320);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(28);

            builder.HasIndex(p => p.Name)
                .IsClustered(false);
        }
    }
}