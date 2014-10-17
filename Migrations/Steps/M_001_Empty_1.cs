using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MigSharp;

namespace Skattedugnad.Migrations.Steps
{
   [MigrationExport(Tag = "Empty")]
   public class M_001_Empty_1 : IReversibleMigration
   {
      public void Up(IDatabase db)
      {
         db.Execute(@"
if object_id('Person') is not null
   drop table Person
");         db.Execute(@"
if object_id('Request') is not null
   drop table Request
");
      }

      public void Down(IDatabase db)
      {
      }
   }
}