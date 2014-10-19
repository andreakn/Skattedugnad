using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Providers.Entities;
using InfoDugnad2.Models;
using Skattedugnad.Models;

namespace Skattedugnad.Data
{
   public class DataLoader
   {

	   public static DataLoader Create()
	   {
		   return new DataLoader(new SqlDatabase(ConfigurationManager.ConnectionStrings["DB"].ConnectionString,false ));
	   }

       private readonly ISqlDatabase _database;

       public DataLoader(ISqlDatabase database)
        {
            _database = database;
        }

      public IEnumerable<Request> GetRequestsForUser(string id)
      {
         return _database.Query<Request>(string.Format("SELECT * FROM Request where RequestedBy = @id and status<>{0}",(int)RequestStatus.Viewed), new { id});
      } 
      public IEnumerable<Request> GetAnswerableCandidatesForUser(string username)
      {
         return _database.Query<Request>(string.Format("SELECT top 50 * FROM Request where status={0} and RequestedBy <> @username order by requestpriority desc, requestedDate asc "
            ,(int)RequestStatus.New), new { username});
      }

	   public Person GetPerson(string username)
	   {
		   return _database.Query<Person>(string.Format("SELECT * FROM Person where username = @username"), new { username }).FirstOrDefault();
	   }

	   public Request GetRequest(int id)
	   {
		   return _database.Query<Request>(string.Format("SELECT * FROM Request where id = @id and status<>{0}", (int)RequestStatus.Viewed), new { id }).FirstOrDefault();
	   }

	   public void RegisterAnswerViewed(int id)
	   {
		   _database.Execute(string.Format("UPDATE Request set status = {0} where id = @id", (int) RequestStatus.Viewed),
			   new {id});
	   }

	   public Request GetAnsweredRequest(int id)
	   {
		   return _database.Query<Request>(string.Format("SELECT * FROM Request where id = @id and status={0}", (int)RequestStatus.Answered), new { id }).FirstOrDefault();
	   }

	   public void RegisterAnswerForRequest(int questionid, string answer, string username)
	   {	
		   _database.Execute(
			   "UPDATE Request SET AnsweredBy = @username, AnswerInfo = @answer, AnsweredDate = @now, Status = @status where id = @questionid",
			   new {questionid, username, answer, now = DateTime.UtcNow, status = (int)RequestStatus.Answered});
	   }

	   public void AdjustPointsForUser(string username, int delta)
	   {
		   var currentScore = (int)_database.ExecuteScalar("SELECT Score from Person where Username = @username", new {username});
		   var newScore = currentScore + delta;
		   _database.Execute("UPDATE Person SET Score = @newScore where Username = @username",new {username,newScore });
	   }

	   public void UndoAnswerForRequest(int questionid)
	   {
		   _database.Execute(
			   "UPDATE Request SET AnsweredBy = null, AnswerInfo = null, AnsweredDate = null, Status=@status where id = @questionid",
			   new { status = (int)RequestStatus.New,questionid});
	   }

	   public void BlockUser(string baduser)
	   {
		   _database.Execute(
			   "UPDATE Person SET IsBlocked = 1  where username = @baduser",
			   new {   baduser });
		   _database.Execute("UPDATE Request SET Status = @blocked where RequestedBy = @baduser",
			   new {blocked = (int) RequestStatus.Viewed, baduser});
	   }

	   public void RegisterComplaintOnUser(string baduser)
	   {
		   var count = ReadComplaintsOnUser(baduser);
		   count++;
		   _database.Execute(
			   "UPDATE Person SET ComplaintCount = @count  where username = @baduser", new {count, baduser});

	   }

	   public int ReadComplaintsOnUser(string baduser)
	   {
		   return (int)_database.ExecuteScalar("SELECT ComplaintCount from Person where Username = @baduser", new { baduser });
		   }

	   public void RegisterHardQuestion(int id)
	   {
		   var count = ReadQuestionHardness(id);
		   count++;
		   _database.Execute("UPDATE Request SET GiveUpCount = @count  where id = @id", new { count, id});
	   }

	   public int ReadQuestionHardness(int id)
	   {
		   return (int)_database.ExecuteScalar("SELECT GiveUpCount from Request where id = @id", new { id });
	   }

	   public void GiveUpOnQuestion(int id)
	   {
		   _database.Execute("Update Request SET Status = @status where id = @id", new {id, status = (int) RequestStatus.TooHard });
	   }

	   public void CreatePerson(string username)
	   {
		   _database.Execute("insert into [Person] (username) values (@username)", new { username });
		   
	   }

	   public void RegisterRequest(string username, string text)
	   {
		   _database.Execute("insert into request (Status,RequestedBy,RequestInfo) values (0,@username,@text)", new { username,text });
	   }
   }
}