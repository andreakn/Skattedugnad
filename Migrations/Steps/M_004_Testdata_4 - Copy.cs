using System.Data;
using MigSharp;

namespace Skattedugnad.Migrations.Steps
{
   [MigrationExport(Tag = "TestData")]
   public class M_004_TestData_4:IReversibleMigration
   {
      public void Up(IDatabase db)
      {
         db.Execute(@"insert into [Person] (username) values ('andreakn')");
         db.Execute(@"insert into [Person] (username) values ('duup')");

         db.Execute(@"insert into request (Status,RequestedBy,RequestInfo) values (0,'andreakn','unanswered')");
         db.Execute(@"insert into request (Status,RequestedBy,RequestInfo,AnsweredBy,AnsweredDate,AnswerInfo) values (10,'andreakn','answered','duup',getdate(),'Some info for you')");
         db.Execute(@"insert into request (Status,RequestedBy,RequestInfo,AnsweredBy,AnsweredDate,AnswerInfo) values (20,'andreakn','viewed','duup',getdate(),'Some info for you')");
         
      }

      public void Down(IDatabase db)
      {
        
      }
   }
}