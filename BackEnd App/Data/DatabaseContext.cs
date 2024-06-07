using BackEnd_App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_App.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>().ToTable("TestEntity");
        }
    }
}
