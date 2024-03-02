using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using N5ChallengeDockerDemo.Models;
using N5ChallengeDockerDemo.Tools;
using Nest;

namespace N5ChallengeDockerDemo.Data
{
    public class ApplicationDbContext : DbContext
    {   

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if(!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

    }
}
