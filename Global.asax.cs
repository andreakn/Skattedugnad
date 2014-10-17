using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Skattedugnad.Migrations;
using Skattedugnad.Migrations.Steps;

namespace Skattedugnad
{
   // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
   // visit http://go.microsoft.com/?LinkId=9394801

   public class MvcApplication : System.Web.HttpApplication
   {
      protected void Application_Start()
      {
         AreaRegistration.RegisterAllAreas();

         WebApiConfig.Register(GlobalConfiguration.Configuration);
         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleConfig.RegisterBundles(BundleTable.Bundles);
         AuthConfig.RegisterAuth();
         MigrateDatabases();
      }



      private static void MigrateDatabases()
      {
     
         new MigSharpMigrator(ConfigurationManager.ConnectionStrings["DB"].ConnectionString, typeof(M_001_Empty_1).Assembly).MigrateUp();
      }
   }
}