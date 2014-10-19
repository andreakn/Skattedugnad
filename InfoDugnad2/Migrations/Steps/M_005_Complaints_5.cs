using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MigSharp;

namespace InfoDugnad2.Migrations.Steps
{
	[MigrationExport(Tag = "Complaints")]
	public class M_005_Complaints_5:IReversibleMigration
	{
		public void Up(IDatabase db)
		{
			db.Tables["Person"]
				.AddNotNullableColumn("ComplaintCount", DbType.Int32).HavingDefault(0)
				.AddNotNullableColumn("IsBlocked", DbType.Boolean).HavingDefault(0);

		}

		public void Down(IDatabase db)
		{
			db.Tables["Person"].Columns["ComplaintCount"].Drop();
			db.Tables["Person"].Columns["IsBlocked"].Drop();
		}
	}
}