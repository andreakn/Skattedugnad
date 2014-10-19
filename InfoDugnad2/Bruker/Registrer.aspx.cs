using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoDugnad2.Innlogget;
using InfoDugnad2.Models;
using Skattedugnad.Data;

namespace InfoDugnad2.Account
{
	public partial class Register : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}


		protected void RegistrerBruker(object sender, EventArgs e)
		{
			var errors = new List<string>();

			if (Password.Text != ConfirmPassword.Text 
				&& !string.IsNullOrEmpty(Password.Text)  
				&& !string.IsNullOrEmpty(ConfirmPassword.Text) )
				errors.Add("Passordene er ikke like");

			if(string.IsNullOrEmpty(Password.Text) || string.IsNullOrEmpty(ConfirmPassword.Text) )
				errors.Add("Du må skrive inn og bekrefte passordet");
			if(Password.Text.Length<8)
				errors.Add("Passordet må være minst 8 bokstaver langt");
			if(string.IsNullOrEmpty(Username.Text))
				errors.Add("Du må skrive inn et brukernavn");

			var dataloader = DataLoader.Create();
			if(dataloader.GetPerson(Username.Text)!=null)
				errors.Add("Det brukernavnet er dessverre tatt, velg et annet");

			if (errors.Any())
			{
				LiteralError.Text = string.Join("<br/>", errors);
				phError.Visible = true;
				return;
			}
			phError.Visible = false;
			Logic.CreatePerson(Username.Text, Password.Text, Email.Text);
			FormsAuthentication.SetAuthCookie(Username.Text, createPersistentCookie: false);
			RedirectToMyPage();
		}
	}
}