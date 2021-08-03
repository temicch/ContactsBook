using AutoMapper;
using ContactsBook.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ContactsBook.Tests.Common
{
    public class AutoMapperConfigurationTests: IClassFixture<CbWebApplicationFactory<Startup>>
    {
        private readonly CbWebApplicationFactory<Startup> _cbWebApplicationFactory;
        private readonly IConfigurationProvider _autoMapperConfigurator;

        public AutoMapperConfigurationTests(CbWebApplicationFactory<Startup> cbWebApplicationFactory)
        {
            _cbWebApplicationFactory = cbWebApplicationFactory; 
            
            _cbWebApplicationFactory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var serviceScope = _cbWebApplicationFactory.ServiceProvider.CreateScope().ServiceProvider;
            _autoMapperConfigurator = serviceScope.GetRequiredService<IConfigurationProvider>();
        }

        [Fact]
        public void Configuration_Valid()
        {
            _autoMapperConfigurator.AssertConfigurationIsValid();
        }
    }
}
