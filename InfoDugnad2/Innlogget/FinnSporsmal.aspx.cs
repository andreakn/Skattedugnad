using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Skattedugnad.Data;

namespace InfoDugnad2.Innlogget
{
	public partial class FinnSporsmal : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = 0;
			var candidates = DataLoader.Create().GetAnswerableCandidatesForUser(CurrentUsername).ToList();
			if (candidates.Count > 0)
			{
				var rand = new Random();
				var index = (int)(rand.Next(candidates.Count));
				var selected = candidates[index];
				id = selected.Id;
			}
			Response.Redirect("/Innlogget/GiSvar?id="+id);
		}
	}
}