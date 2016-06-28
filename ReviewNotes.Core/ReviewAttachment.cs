using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNotes.Core
{
    // rename, think of a more appropriate name
    public class ReviewAttachment
    {
        public int Id { get; set; }

        public string Filename { get; set; }

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; }

        public virtual Review Review { get; set; }
    }
}
