using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MigSharp;

namespace InfoDugnad2.Migrations.Steps
{
	[MigrationExport(Tag="Email")]
	public class M_009_Email_9:IReversibleMigration
	{
		public void Up(IDatabase db)
		{
			db.Tables["Person"]
				.AddNullableColumn("Email", DbType.AnsiString).OfSize(100);

		}

		public void Down(IDatabase db)
		{
			db.Tables["Person"].Columns["Email"].Drop();
		}
	}
}