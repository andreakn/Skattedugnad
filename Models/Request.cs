using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Skattedugnad.Models
{

   public enum RequestStatus
   {
      New=0,
      Answered=10,
      Viewed=20
   }

   public class Request
   {
      public int Id { get; set; }
      public RequestStatus Status { get; set; }
      public int RequestedPriority { get; set; }

      public int RequestedBy { get; set; }
      public DateTime RequestedDate { get; set; }
      public String RequestInfo { get; set; }

      public int AnsweredBy { get; set; }
      public DateTime AnsweredDate { get; set; }
      public String AnswerInfo { get; set; }

      public bool AnswerAcceptable { get; set; }
      public bool AnswerFlagged{ get; set; }

   
   
   }
}