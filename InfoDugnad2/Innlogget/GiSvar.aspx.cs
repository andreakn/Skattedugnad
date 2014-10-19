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
	public partial class GiSvar : BasePage
	{
		public Request RequestedAnswer { get; set; }
		private DataLoader dataLoader;
		protected void Page_Load(object sender, EventArgs e)
		{
			dataLoader = DataLoader.Create();

			if (!IsPostBack)
			{
				LoadQuestion();
			}
		}

		private void LoadQuestion()
		{
			var idstring = Request.Params["id"];
			int id;
			if (int.TryParse(idstring, out id))
			{
				RequestedAnswer = dataLoader.GetRequest(id);
			}
			if (RequestedAnswer != null)
			{
				QuestionId.Value = "" + RequestedAnswer.Id;
				SessionCookie.Value = "" + RequestedAnswer.Id.GetHashCode() / 2;
			}
		}

		protected void RegisterAnswer(object sender, EventArgs e)
		{
			var id = ValidateQuestionIdIntact();
			if (id.HasValue)
			{
				Logic.UserAnsweredQuestion(CurrentUsername,id.Value,TextBoxAnswerInfo.Text);
			}
			RedirectToMyPage();
		}

		private int? ValidateQuestionIdIntact()
		{
			int id, sessionCookie;
			if (int.TryParse(QuestionId.Value, out id) && int.TryParse(SessionCookie.Value, out sessionCookie))
			{
				if (sessionCookie == (id.GetHashCode()/2))
					return id;
			}
			return null;
		}

		protected void RegisterCantAnswer(object sender, EventArgs e)
		{
			var id = ValidateQuestionIdIntact();
			if (id.HasValue)
				Logic.UserStumpedOnQuestion(CurrentUsername, id.Value);
			RedirectToMyPage();
		}

		protected void RegisterComplaint(object sender, EventArgs e)
		{
			var id = ValidateQuestionIdIntact();
			if(id.HasValue)
				Logic.UserComplainsOnQuestion(CurrentUsername,id.Value);
			RedirectToMyPage();
		}

		protected void FindNewRequest(object sender, EventArgs e)
		{
			Response.Redirect("/Innlogget/FinnSporsmal");
		}
	}
}