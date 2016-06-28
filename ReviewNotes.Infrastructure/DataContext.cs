using ReviewNotes.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNotes.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DbSet<ReviewAttachment> Attachments { get; set; }
    }
}
