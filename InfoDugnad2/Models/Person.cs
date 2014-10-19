using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoDugnad2.Models
{
	public class Person
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public int Score { get; set; }
		public bool IsAbusive { get; set; }
		public bool IsBlocked { get; set; }
		public string PasswordSalt { get; set; }
		public string PasswordHash { get; set; }
		public string PasswordBacon { get; set; }
		public string Email { get; set; }

	}
}