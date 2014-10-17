using System.Data;
using MigSharp;

namespace Skattedugnad.Migrations.Steps
{
   [MigrationExport(Tag = "Initial scheme")]
   public class M_002_InitialModel_2:IReversibleMigration
   {
      public void Up(IDatabase db)
      {
         db.CreateTable("User")
            .WithNotNullableColumn("Id", DbType.Int32).AsIdentity()
            .WithNullableColumn("Username", DbType.AnsiString).OfSize(50);
         db.CreateTable("Request")
            .WithNotNullableColumn("Id", DbType.Int32).AsIdentity()
            .WithNotNullableColumn("Status", DbType.Int32)
            .WithNotNullableColumn("RequestPriority", DbType.Int32)
            .WithNotNullableColumn("RequestedBy", DbType.Int32)
            .WithNotNullableColumn("RequestedDate", DbType.DateTime)
            .WithNullableColumn("RequestInfo", DbType.AnsiString).OfSize(255)
            .WithNullableColumn("AnsweredBy", DbType.Int32)
            .WithNullableColumn("AnsweredDate", DbType.DateTime)
            .WithNullableColumn("AnswerInfo", DbType.AnsiString).OfSize(255)
            .WithNullableColumn("AnswerAcceptable", DbType.Boolean)
            .WithNullableColumn("AnswerFlagged", DbType.Boolean)
            ;
      }

      public void Down(IDatabase db)
      {
         db.Tables["User"].Drop();
         db.Tables["Request"].Drop();
      }
   }
}