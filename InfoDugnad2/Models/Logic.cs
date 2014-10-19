using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using Skattedugnad.Data;

namespace InfoDugnad2.Models
{
	public static class Logic
	{
		public static void UserAnsweredQuestion(string username, int questionid, string text)
		{
			var dataloader = DataLoader.Create();
			dataloader.RegisterAnswerForRequest(questionid, text, username);
			dataloader.AdjustPointsForUser(username,1);

		}

		public static void UserNotSatisfiedByAnswer(string currentUsername, int id)
		{
			var dataloader = DataLoader.Create();
			var request = dataloader.GetRequest(id);
			if (request.RequestedBy != currentUsername)
			{
				//somthing muffens
				return;
			}
			dataloader.AdjustPointsForUser(request.AnsweredBy,-1); //take away the point earned for the answer
			dataloader.UndoAnswerForRequest(id);
		}

		public static void UserComplainsOnAnswer(string currentUsername, int id)
		{
			var dataloader = DataLoader.Create();
			var request = dataloader.GetRequest(id);
			if (request.RequestedBy != currentUsername)
			{
				//somthing muffens
				return;
			}
			RegisterComplaintOnUser(dataloader, request.AnsweredBy);
			dataloader.UndoAnswerForRequest(id);
		}


		public static void UserComplainsOnQuestion(string currentUsername, int id)
		{
			var dataloader = DataLoader.Create();
			var request = dataloader.GetRequest(id);
			
			RegisterComplaintOnUser(dataloader, request.RequestedBy);

			
			dataloader.UndoAnswerForRequest(id);
		}

		private static void RegisterComplaintOnUser(DataLoader dataloader, string baduser)
		{
			dataloader.AdjustPointsForUser(baduser, -6); //Penalize user for harassing community
		
			dataloader.RegisterComplaintOnUser(baduser); //Penalize user for harassing community
			if (dataloader.ReadComplaintsOnUser(baduser) > 2) //user is bad user
			{
				dataloader.BlockUser(baduser);
			}; 
		}

		public static void UserStumpedOnQuestion(string currentUsername, int id)
		{
			var dataloader = DataLoader.Create();
			dataloader.RegisterHardQuestion(id);
			if (dataloader.ReadQuestionHardness(id) > 2)
			{
				dataloader.GiveUpOnQuestion(id);
			}			
		}

		public static Person LoadPerson(string username)
		{
			var dataloader = DataLoader.Create();
			var person = dataloader.GetPerson(username);
			if (person == null)
				dataloader.CreatePerson(username);
			person = person ?? dataloader.GetPerson(username);
			return person;
		}

		public static void RegisterRequest(string username, string text)
		{
			var dataloader = DataLoader.Create();
			dataloader.RegisterRequest(username, text);
			dataloader.AdjustPointsForUser(username, -1); //cost of asking
		
		}
	}
}