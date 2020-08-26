using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace WebApplication2
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "vuelidate",
                new ScriptResourceDefinition
                {
                    Path = "~/node_modules/vuelidate/dist/vuelidate.min.js",
                    DebugPath = "~/node_modules/vuelidate/dist/vuelidate.min.js",
                });
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "materialize",
                new ScriptResourceDefinition
                {
                    Path = "~/node_modules/materialize-css/dist/js/materialize.min.js",
                    DebugPath = "~/node_modules/materialize-css/dist/js/materialize.js",
                });
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "vue",
                new ScriptResourceDefinition
                {
                    Path = "~/node_modules/vue/dist/vue.min.js",
                    DebugPath = "~/node_modules/vue/dist/vue.js",
                });
            ScriptManager.ScriptResourceMapping.AddDefinition(
                "axios",
                new ScriptResourceDefinition
                {
                    Path = "~/node_modules/axios/dist/axios.min.js",
                    DebugPath = "~/node_modules/axios/dist/axios.js",
                });

            //ScriptManager.ScriptResourceMapping.AddDefinition("myscripts", new ScriptResourceDefinition
            //{
            //    Path = "~/Scripts/dist/main.build.min.js",
            //    DebugPath = "~/Scripts/dist/main.build.js"
            //});

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
        }
    }
}