using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoDugnad2.Innlogget;
using InfoDugnad2.Models;

namespace InfoDugnad2.Account
{
	public partial class Login : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RegisterHyperLink.NavigateUrl = "Registrer";

			var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
			if (!String.IsNullOrEmpty(returnUrl))
			{
				RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
			}
		}

		protected void Login_Click(object sender, EventArgs e)
		{
			phError.Visible = false;
			LiteralError.Text = "";
			var errors = new List<string>();

			if (string.IsNullOrEmpty(UserName.Text))
				errors.Add("Du må skrive inn et brukernavn");

			if (string.IsNullOrEmpty(Password.Text))
				errors.Add("Du må skrive inn et passord");

			if (!errors.Any())
			{
				var person = Logic.TryToLoginPerson(UserName.Text, Password.Text);
				if (person == null)
				{
					errors.Add("Brukernavn eller passord er feil.");
				}
			}

			if (errors.Any())
			{
				phError.Visible = true;
				LiteralError.Text = string.Join("<br/>", errors);
			}
			else
			{
				FormsAuthentication.SetAuthCookie(UserName.Text, createPersistentCookie: false);
				RedirectToMyPage(); 
			}
			
		}
	}
}