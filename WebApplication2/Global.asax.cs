using Application.DAL.Entity;
using AutoMapper;
using AutoMapper.Data;
using System;
using System.Data;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using Application.BLL.Services;
using Application.DAL.Repository;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.WebApi;

namespace WebApplication2
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;
        public IContainerProvider ContainerProvider => _containerProvider;
        private static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyContacts;Integrated Security=True";

        void Application_Start(object sender, EventArgs e)
        {
            _containerProvider = CreateContainer();
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static ContainerProvider CreateContainer()
        {

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var mapper = ConfigureMapper();

            builder.Register(x => new ContactRepository(connectionString, x.Resolve<IMapper>()))
                .As<IContactRepository>();
            builder.RegisterType<ContactsService>()
                .As<IContactsService>();
            builder.Register(x => mapper)
                .As<IMapper>();
            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return new ContainerProvider(container);
        }

        private static Mapper ConfigureMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping(false);
                cfg.CreateMap<IDataRecord, Contact>();
            });
            return new Mapper(configuration);
        }
    }
}