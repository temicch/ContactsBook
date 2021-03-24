using System;
using System.Web;
using System.Web.Routing;

namespace ContactsBook.WebUI
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {

            //GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

    }
}