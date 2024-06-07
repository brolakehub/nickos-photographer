using BackEnd_App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_App.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumCategory> AlbumCategories { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Models.Entities.File> Files { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Service> Services { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>();
            modelBuilder.Entity<AlbumCategory>();
            modelBuilder.Entity<ContactRequest>();
            modelBuilder.Entity<Models.Entities.File>();
            modelBuilder.Entity<Info>();
            modelBuilder.Entity<Service>();
        }
    }
}
