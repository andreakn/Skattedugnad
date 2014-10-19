using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace InfoDugnad2.Innlogget
{
	public class BasePage:Page
	{
		public string CurrentUsername
		{
			get
			{
				return HttpContext.Current.User.Identity.Name;
			}
		}

		public void RedirectToMyPage()
		{
			Response.Redirect("/Innlogget/MinSide");
		}
	}
}