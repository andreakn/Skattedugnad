using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MigSharp;

namespace InfoDugnad2.Migrations.Steps
{
	[MigrationExport(Tag="Cleanup")]
	public class M_008_Cleanup_8:IReversibleMigration
	{
		public void Up(IDatabase db)
		{
			db.Execute("delete from Request");
			db.Execute("delete from Person");
		}

		public void Down(IDatabase db)
		{
		}
	}
}