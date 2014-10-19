using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MigSharp;

namespace InfoDugnad2.Migrations.Steps
{
	[MigrationExport(Tag="Hardness")]
	public class M_006_Hardness_6:IReversibleMigration
	{
		public void Up(IDatabase db)
		{
			db.Tables["Request"].AddNotNullableColumn("GiveUpCount", DbType.Int32).HavingDefault(0);
		}

		public void Down(IDatabase db)
		{
			db.Tables["Request"].Columns["GiveUpCount"].Drop();
		}
	}
}