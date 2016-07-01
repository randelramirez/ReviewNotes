using ReviewNotes.Core;
using ReviewNotes.Infrastructure;
using ReviewNotes.WebUI.Helper;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ReviewNotes.WebUI.Controllers
{
    public class ReviewsController : Controller
    {
        private DataContext dataContext = new DataContext();

        // GET: Reviews
        public ActionResult Index()
        {
            return View(this.dataContext.Reviews);
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
                foreach (var file in files)
                {
                    //byte[] fileContent = file.InputStream.StreamToByteArray(file.ContentLength);
                    byte[] fileContent = file.ToByteArray();
                    var attachment = new ReviewAttachment
                    {
                        Filename = file.FileName,
                        ContentType = file.ContentType,
                        FileContent = fileContent
                    };
                    review.ReviewAttachments.Add(attachment);
                }

                this.dataContext.Reviews.Add(review);
                this.dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }
    }
}