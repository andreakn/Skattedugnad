using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoDugnad2.Innlogget;
using InfoDugnad2.Models;
using Skattedugnad.Data;
using Skattedugnad.Models;

namespace InfoDugnad2
{
	public partial class MinSide : BasePage
	{

		protected List<Request> UnansweredRequests { get; set; } 
		protected List<Request> AnsweredRequests { get; set; } 
		protected List<Request> TooHardRequests { get; set; } 
		protected Person Me { get; set; }


		protected void Page_Load(object sender, EventArgs e)
		{
			var dataloader = DataLoader.Create();
			var username = CurrentUsername;
			var usersRequests = dataloader.GetRequestsForUser(username).ToList();
			UnansweredRequests = usersRequests.Where(s => s.Status==RequestStatus.New).ToList();
			AnsweredRequests = usersRequests.Where(s => s.Status==RequestStatus.Answered).ToList();
			TooHardRequests = usersRequests.Where(s => s.Status==RequestStatus.TooHard).ToList();
			Me = Logic.LoadPerson(username);

		}
	}
}