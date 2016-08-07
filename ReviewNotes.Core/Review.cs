using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewNotes.Core
{
    public class Review
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

    }
}
