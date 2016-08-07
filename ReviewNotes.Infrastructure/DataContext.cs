using ReviewNotes.Core;
using System.Data.Entity;

namespace ReviewNotes.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasKey(r => r.Id)
                .HasMany(r => r.Attachments)
                .WithRequired(r => r.Review)
                .HasForeignKey(r => r.ReviewId);
            modelBuilder.Entity<Review>()
                .Property(r => r.Content)
                .HasColumnType("nText");

            modelBuilder.Entity<Attachment>().HasKey(a => a.Id);
        }
    }
}
