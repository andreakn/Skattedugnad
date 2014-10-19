using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MigSharp;

namespace InfoDugnad2.Migrations.Steps
{
	[MigrationExport(Tag="Passwords")]
	public class M_007_Password_7:IReversibleMigration
	{
		public void Up(IDatabase db)
		{
			db.Tables["Person"].AddNullableColumn("PasswordHash", DbType.String).OfSize(512);
			db.Tables["Person"].AddNullableColumn("PasswordSalt", DbType.String).OfSize(512);
			db.Tables["Person"].AddNullableColumn("PasswordBacon", DbType.String).OfSize(512);
		}

		public void Down(IDatabase db)
		{
			db.Tables["Person"].Columns["PasswordHash"].Drop();
			db.Tables["Person"].Columns["PasswordSalt"].Drop();
			db.Tables["Person"].Columns["PasswordBacon"].Drop();
		}
	}
}