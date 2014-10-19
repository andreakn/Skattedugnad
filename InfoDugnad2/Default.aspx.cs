using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoDugnad2.Innlogget;

namespace InfoDugnad2
{
	public partial class _Default : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(CurrentUsername))
			{
				Response.Redirect("/Bruker/Innlogging");
			}
			else
			{
				Response.Redirect("/Innlogget/MinSide");
			}
		}
	}
}