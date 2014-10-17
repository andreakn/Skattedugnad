using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Skattedugnad.Data;

namespace Skattedugnad.Controllers
{
   [Authorize]
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         ViewBag.Message = "Dine forespørsler";
         var dataloader =
            new DataLoader(new SqlDatabase(ConfigurationManager.ConnectionStrings["DB"].ConnectionString, false));

         var userName = this.Request.RequestContext.HttpContext.User.Identity.Name;
         var activeRequests = dataloader.GetRequestsForUser(userName);
         return View(activeRequests);
      }

      public ActionResult About()
      {
         ViewBag.Message = "Your app description page.";

         return View();
      }

      public ActionResult Contact()
      {
         ViewBag.Message = "Your contact page.";

         return View();
      }
   }
}
