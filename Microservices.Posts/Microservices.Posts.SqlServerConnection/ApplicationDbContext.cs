using Microservices.Posts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Posts.SqlServerConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PostEntity> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostEntity>(entity =>
            {
                entity.ToTable("Posts");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.Content)
                      .IsRequired();

                entity.Property(e => e.Author)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");
            });
        }

    }
}
