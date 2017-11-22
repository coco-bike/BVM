using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Base.Mapping;

namespace Model
{
    public class MyContext : DbContext
    {
        public MyContext()
            : base("name=BVM")
        {
            Database.SetInitializer<MyContext>(new MigrateDatabaseToLatestVersion<MyContext, ReportingDbMigrationsConfiguration>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Authority> Authority { get; set; }
        public DbSet<Role> Role { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new SaleMap());
            modelBuilder.Configurations.Add(new MachineMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new AuthorityMap());
        }
        internal sealed class ReportingDbMigrationsConfiguration : System.Data.Entity.Migrations.DbMigrationsConfiguration<MyContext>
        {
            public ReportingDbMigrationsConfiguration()
            {
                AutomaticMigrationsEnabled = true;//任何Model Class的修改將會直接更新DB
                AutomaticMigrationDataLossAllowed = true;
            }
        }
    }
}
