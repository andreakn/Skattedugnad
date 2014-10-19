using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoDugnad2.Models;
using Skattedugnad.Data;

namespace InfoDugnad2.Innlogget
{
	public partial class StillSporsmal : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var me = DataLoader.Create().GetPerson(CurrentUsername);
				if(me.IsBlocked)
					RedirectToMyPage();
				if(me.Score<=0)
					RedirectToMyPage();
			}
		}

		protected void Ask(object sender, EventArgs e)
		{
			Logic.RegisterRequest(CurrentUsername, TextBoxQuestion.Text);
			RedirectToMyPage();
		}
	}
}