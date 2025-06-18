using Microservices.Comments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Comments.SqlServerConnection
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CommentEntity>(entity =>
            {
                entity.ToTable("Comments");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(c => c.PostId)
                      .IsRequired();

                entity.Property(c => c.Author)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(c => c.Content)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(c => c.CreatedAt)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }

    }

}
