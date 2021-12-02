using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TestSystem.Webapi.Models.Tables;

namespace TestSystem.Webapi.Models
{
    public class TestSystemContext : DbContext
    {
        public TestSystemContext() : base("TestSystemContext")
        {
        }

        public DbSet<T_Account> T_Accounts { get; set; }
        public DbSet<T_AccountLevel> T_AccountLevels { get; set; }
        public DbSet<T_LoginRecord> T_LoginRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}