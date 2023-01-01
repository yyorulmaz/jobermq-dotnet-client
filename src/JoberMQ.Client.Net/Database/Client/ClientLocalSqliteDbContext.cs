using JoberMQ.Client.Net.Mappings.DBREALY.EF;
using JoberMQ.Common.Dbos;
using Microsoft.EntityFrameworkCore;

namespace JoberMQ.Client.Net.Database.Client
{
    //----------------------LocalSqliteDbContext-------------------------
    //Add-Migration StartLocalDb -OutputDir Database/Local/Migrations -Context LocalSqliteDbContext -Project JoberMQ.DataAccess
    //update-database -Project JoberMQ.DataAccess -Context LocalSqliteDbContext
    //startup project JoberMQ.DataAccess
    //package manager console  Timer project : JoberMQ.DataAccess

    internal class ClientLocalSqliteDbContext : DbContext
    {
        public static string ConnectionString { internal get; set; }

        public DbSet<ClientLocalDataDbo> LocalData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=LocalDb.db", sqlServerOptions => sqlServerOptions.CommandTimeout(240));
            optionsBuilder.UseSqlite(ConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(240));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // MAPPİNG
            modelBuilder.ApplyConfiguration(new EfClientLocalDataMap());
        }
    }
}


