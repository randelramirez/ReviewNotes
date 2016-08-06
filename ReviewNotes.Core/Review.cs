using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewNotes.Core
{
    public class Review
    {
        public int Id { get; set; }

        public string Title { get; set; }

        // use a viewmodel and put the attributes their
        //[DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public virtual ICollection<Attachment> ReviewAttachments { get; set; }

    }
}
