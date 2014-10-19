using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
			RegisterComplaintOnUser(dataloader, request.AnsweredBy, currentUsername);
			dataloader.UndoAnswerForRequest(id);
		}


		public static void UserComplainsOnQuestion(string currentUsername, int id)
		{
			var dataloader = DataLoader.Create();
			var request = dataloader.GetRequest(id);
			
			RegisterComplaintOnUser(dataloader, request.RequestedBy, currentUsername);

			
			dataloader.UndoAnswerForRequest(id);
		}

		private static void RegisterComplaintOnUser(DataLoader dataloader, string baduser, string complainer)
		{
			dataloader.AdjustPointsForUser(baduser, -6); //Penalize user for harassing community
			dataloader.AdjustPointsForUser(complainer, -1); //stop whining trolls from trolling
		
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

		public static void CreatePerson(string username, string password, string email)
		{
			var dataloader = DataLoader.Create();
			var passwordSalt = GetSalt();
			var hash = GetHash(password, passwordSalt);
			var bacon = password;
			dataloader.RegisterPerson(username, hash,passwordSalt,bacon);

		}

		private static string GetSalt()
		{
			byte[] salt = new byte[100];
			var cryptoRand = RandomNumberGenerator.Create();
			cryptoRand.GetNonZeroBytes(salt);
			return salt.Aggregate(string.Empty, (current, x) => current + String.Format("{0:x2}", x));
		}

		private static byte[] Combine(byte[] a, byte[] b)
		{
			var c = new byte[a.Length + b.Length];
			Buffer.BlockCopy(a, 0, c, 0, a.Length);
			Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
			return c;
		}

		public static Person TryToLoginPerson(string username, string password)
		{
			var dataloader = DataLoader.Create();
			var person = dataloader.GetPerson(username);
			if (person == null) return null;

			var hash = GetHash(password, person.PasswordSalt);
			if (hash.Equals(person.PasswordHash))
				return person;

			return null;
		}


		public static string GetHash(string password, string salt)
		{
			var pbytes = Combine(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt));
			var hasher = new SHA256Managed();
			var hash = hasher.ComputeHash(pbytes);

			return hash.Aggregate(string.Empty, (current, x) => current + String.Format("{0:x2}", x));
		}
	}
}