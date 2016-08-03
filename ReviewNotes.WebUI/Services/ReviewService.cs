using ReviewNotes.Core;
using ReviewNotes.Infrastructure;
using ReviewNotes.WebUI.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReviewNotes.WebUI.Services
{
    public class ReviewService
    {
        private DataContext dataContext;

        public ReviewService(DataContext dataContext )
        {
            this.dataContext = dataContext;
        }

        public IEnumerable<Review> GetAllReviews()
        {
            return this.dataContext.Reviews.ToList();
        }

        public void Add(Review review)
        {
            this.dataContext.Reviews.Add(review);
        }

        public void Update(Review postedReview, IEnumerable<HttpPostedFileBase> files,
            IEnumerable<int> deletedAttachments)
        {
            var review = this.dataContext.Reviews.Single(r => r.Id == postedReview.Id);
            review.Title = postedReview.Title;
            review.Content = postedReview.Content;
            if (deletedAttachments != null && deletedAttachments.Count() > 0)
            {
                foreach (var attachmentId in deletedAttachments)
                {
                    var attachment = this.dataContext.Attachments.Find(attachmentId);
                    if (attachment != null)
                    {
                        this.dataContext.Attachments.Remove(attachment);
                    }
                }
            }
            if (deletedAttachments != null && files.Count() > 0)
            {
                foreach (var file in files)
                {
                    byte[] fileContent = file?.ToByteArray();
                    var attachment = new Attachment
                    {
                        Filename = file.FileName,
                        ContentType = file.ContentType,
                        FileContent = fileContent
                    };
                    review.ReviewAttachments.Add(attachment);
                }
            }

            this.dataContext.Entry(review).State = EntityState.Modified;
        }

        public void Delete(Review review)
        {
            this.dataContext.Reviews.Remove(review);
        }

        public Review GetOne(int id)
        {
            return this.dataContext.Reviews.Find(id);
        }

        public void SaveChanges()
        {
            this.dataContext.SaveChanges();
        }

        public static List<Attachment> UploadedFilesToReviewAttachment(IEnumerable<HttpPostedFileBase> files)
        {
            var result = new List<Attachment>();
            foreach (var file in files)
            {

                byte[] fileContent = file?.ToByteArray();
                var attachment = new Attachment
                {
                    Filename = file.FileName,
                    ContentType = file.ContentType,
                    FileContent = fileContent
                };
                result.Add(attachment);
            }
            return result;
        }
    }
}