using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoDugnad2.Models;
using Skattedugnad.Data;
using Skattedugnad.Models;

namespace InfoDugnad2.Innlogget
{
	public partial class Svar : BasePage
	{
		private DataLoader dataLoader;
		public Request Answer { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				dataLoader = DataLoader.Create();
				LoadAnswer();
				if (Answer != null)
				{
					TextBoxAnswer.Text = Answer.AnswerInfo;
					AnswerId.Value = ""+Answer.Id;
					//RegisterAnswerViewed();
				}
			}
		}

		private void RegisterAnswerViewed()
		{
			dataLoader.RegisterAnswerViewed(Answer.Id);
		}

		private void LoadAnswer()
		{
			var idstring = Request.Params["id"];
			int id;
			if (int.TryParse(idstring, out id))
			{
				Answer = dataLoader.GetAnsweredRequest(id);
				if (Answer.RequestedBy != CurrentUsername)
				{
					Answer = null;
				}
			}
		}

		protected void AskAgain(object sender, EventArgs e)
		{
			Logic.UserNotSatisfiedByAnswer(CurrentUsername, int.Parse(AnswerId.Value));
			RedirectToMyPage();
		}
		protected void Complain(object sender, EventArgs e)
		{
			Logic.UserComplainsOnAnswer(CurrentUsername, int.Parse(AnswerId.Value));
			RedirectToMyPage();	
		}
	}
}