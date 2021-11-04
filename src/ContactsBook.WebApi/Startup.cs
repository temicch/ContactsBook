using System;
using System.IO;
using System.Reflection;
using Bogus;
using ContactsBook.Application;
using ContactsBook.Application.Interfaces.Services;
using ContactsBook.Application.Services;
using ContactsBook.DataAccess.MsSql;
using ContactsBook.DataAccess.MsSql.Repository;
using ContactsBook.Domain.Entities;
using ContactsBook.Infrastructure.Interfaces;
using ContactsBook.Infrastructure.Interfaces.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ContactsBook.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers()
                .AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ContactsBook.WebApi", Version = "v1"});

                var filePath = Path.Combine(AppContext.BaseDirectory, "ContactsBook.WebApi.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddDbContext<ContactsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IContactRepository<Contact>, ContactRepository>();

            services.AddTransient<IContactsService, ContactsService>();
            services.AddTransient<IFakeDataGenerator<Contact>, ContactsSeed>();
            services.AddTransient<Faker<Contact>>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(typeof(ApplicationMapping), typeof(WebApiMapping));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactsBook.WebApi v1"));

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}