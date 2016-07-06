using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNotes.Core
{
    public class Review
    {
        public Review()
        {
            // for lazy loading
            this.ReviewAttachments = new HashSet<ReviewAttachment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        // use a viewmodel and put the attributes their
        //[DataType(DataType.MultilineText)]
        // use fluent api 
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public virtual ICollection<ReviewAttachment> ReviewAttachments { get; set; }

    }
}
