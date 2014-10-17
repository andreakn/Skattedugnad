using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skattedugnad.Models;

namespace Skattedugnad.Data
{
   public class DataLoader
   {

       private readonly ISqlDatabase _database;

       public DataLoader(ISqlDatabase database)
        {
            _database = database;
        }

      public IEnumerable<Request> GetRequestsForUser(string id)
      {
         return _database.Query<Request>(string.Format("SELECT * FROM Request where RequestedBy = @id and status<>{0}",(int)RequestStatus.Viewed), new { id});
      } 
      public IEnumerable<Request> GetAnswerableCandidatesForUser(int id)
      {
         return _database.Query<Request>(string.Format("SELECT top 50 * FROM Request where status={0} and RequestedBy <> @id order by requestpriority desc, requestedDate asc "
            ,(int)RequestStatus.New), new { id});
      } 
   }
}