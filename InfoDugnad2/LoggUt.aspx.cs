using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoDugnad2.Innlogget;

namespace InfoDugnad2
{
	public partial class LoggUt : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
			RedirectToMyPage();
		}
	}
}