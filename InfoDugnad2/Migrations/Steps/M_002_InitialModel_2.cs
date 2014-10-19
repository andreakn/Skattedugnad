using System.Data;
using MigSharp;

namespace Skattedugnad.Migrations.Steps
{
   [MigrationExport(Tag = "Initial scheme")]
   public class M_002_InitialModel_2:IReversibleMigration
   {
      public void Up(IDatabase db)
      {


         db.CreateTable("Person")
            .WithPrimaryKeyColumn("Id", DbType.Int32).AsIdentity()
            .WithNullableColumn("Username", DbType.AnsiString).OfSize(50)
            .WithNotNullableColumn("Score", DbType.Int32).HavingDefault(1)
            .WithNotNullableColumn("IsAbusive", DbType.Boolean).HavingDefault(0);
            
            ;

         db.CreateTable("Request")
            .WithPrimaryKeyColumn("Id", DbType.Int32).AsIdentity()
            .WithNotNullableColumn("Status", DbType.Int32)
            .WithNotNullableColumn("RequestPriority", DbType.Int32).HavingDefault(5)
            .WithNotNullableColumn("RequestedBy", DbType.AnsiString).OfSize(50)
            .WithNotNullableColumn("RequestedDate", DbType.DateTime).HavingCurrentDateTimeAsDefault()
            .WithNotNullableColumn("RequestInfo", DbType.AnsiString).OfSize(255)
            .WithNullableColumn("AnsweredBy", DbType.AnsiString).OfSize(50)
            .WithNullableColumn("AnsweredDate", DbType.DateTime)
            .WithNullableColumn("AnswerInfo", DbType.AnsiString).OfSize(4000)
            .WithNullableColumn("AnswerAcceptable", DbType.Boolean)
            .WithNullableColumn("AnswerFlagged", DbType.Boolean)
            ;
      }

      public void Down(IDatabase db)
      {
         db.Tables["Person"].Drop();
         db.Tables["Request"].Drop();
      }
   }
}