using ReviewNotes.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReviewNotes.WebUI.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}