using ReviewNotes.Core;
using System.Data.Entity;

namespace ReviewNotes.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Attachment> Attachments { get; set; }
    }
}
