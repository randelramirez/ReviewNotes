using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNotes.Core
{
    public class Review
    {
        public int Id { get; set; }

        public string Title { get; set; }

        //Summary
        public string Keynotes { get; set; }

        public virtual ICollection<ReviewAttachment> ReviewAttachments { get; set; }

    }
}
