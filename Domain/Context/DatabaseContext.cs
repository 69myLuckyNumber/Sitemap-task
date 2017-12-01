using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Context
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext() : base("DatabaseConnection")
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
		}

		public DbSet<Host> Hosts { get; set; }

		public DbSet<Page> Pages { get; set; }
	}
}
