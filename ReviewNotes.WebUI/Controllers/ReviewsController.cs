using ReviewNotes.Core;
using ReviewNotes.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ReviewNotes.WebUI.Controllers
{
    public class ReviewsController : Controller
    {
        private DataContext dataContext = new DataContext();

        // GET: Reviews
        public ActionResult Index()
        {
            // using include works
            // but navigation properties are marked as virtual so this should not be required, double check why?????
            var model = this.dataContext.Reviews;//.Include(r => r.ReviewAttachments );
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Review review, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                review.ReviewAttachments = new List<ReviewAttachment>();
                //save
                //code for saving
                foreach (var file in files)
                {

                    // convert file to byte array
                    using (var reader = new System.IO.BinaryReader(file.InputStream))
                    {
                        byte[] fileContent = reader.ReadBytes(file.ContentLength);
                        var attachment = new ReviewAttachment { Filename = file.FileName, ContentType = file.ContentType, FileContent = fileContent };
                        review.ReviewAttachments.Add(attachment);
                    }
                }
                this.dataContext.Reviews.Add(review);
                this.dataContext.SaveChanges();
                //post-redirect-pattern
                return RedirectToAction("Index");
            }

            return View(review);
        }
    }
}