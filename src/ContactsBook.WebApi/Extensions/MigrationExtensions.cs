using System;
using System.Linq;
using ContactsBook.DataAccess.MsSql;
using ContactsBook.Domain.Entities;
using ContactsBook.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ContactsBook.WebApi.Extensions;

internal static class MigrationExtensions
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ContactsDbContext>();

            try
            {
                if (context.Database.IsSqlServer())
                {
                    var isDbExists = (context.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)
                        ?.Exists();

                    context.Database.Migrate();

                    if (isDbExists != true)
                        SeedData(services, context);
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                throw;
            }
        }

        return host;
    }

    private static void SeedData(IServiceProvider serviceProvider, ContactsDbContext contactsDbContext)
    {
        var fakeGenerator = serviceProvider.GetRequiredService<IFakeDataGenerator<Contact>>();

        var contacts = fakeGenerator.Generate(100).ToList();

        contactsDbContext.Contacts.AddRange(contacts);
        contactsDbContext.SaveChanges();
    }
}
