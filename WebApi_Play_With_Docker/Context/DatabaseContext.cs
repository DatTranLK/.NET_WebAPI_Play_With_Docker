using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;
using WebApi_Play_With_Docker.Models;

namespace WebApi_Play_With_Docker.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
			try
			{
				var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
				if(databaseCreator != null)
				{
					if (!databaseCreator.CanConnect()) databaseCreator.Create();
					if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
        }
        public DbSet<User> Users { get; set; }
    }
}
