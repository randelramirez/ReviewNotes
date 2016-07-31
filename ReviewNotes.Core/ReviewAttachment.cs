﻿using System.Collections.Generic;

namespace ReviewNotes.Core
{
    // rename, think of a more appropriate name
    public class Attachment
    {
        public int Id { get; set; }

        public int ReviewId { get; set; }

        public string Filename { get; set; }

        public byte[] FileContent { get; set; }

        public string ContentType { get; set; }

        public virtual Review Review { get; set; }
    }
}
