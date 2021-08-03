using System.IO;
using ContactsBook.DataAccess.MsSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsBook.Tests.Common
{
    public class CbWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public ServiceProvider ServiceProvider { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            builder.ConfigureServices(services =>
            {
                ServiceProvider = services.BuildServiceProvider();

                DbInit();
            });
        }

        protected void DbInit()
        {
            using var scope = ServiceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();

            context.Database.EnsureDeleted();
            context.Database.Migrate();
            DbClear();
        }

        public void DbClear()
        {
            using var scope = ServiceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();

            context.Database.ExecuteSqlRaw("DELETE FROM CONTACTS");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            using var scope = ServiceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();

            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}