using ReviewNotes.Core;
using ReviewNotes.Infrastructure;
using ReviewNotes.WebUI.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                if (files.Count() > 0 && files.ToList()[0] != null)
                {
                    review.ReviewAttachments = new List<ReviewAttachment>();
                    foreach (var file in files)
                    {
                        //byte[] fileContent = file.InputStream.StreamToByteArray(file.ContentLength);
                        byte[] fileContent = file?.ToByteArray();
                        var attachment = new ReviewAttachment
                        {
                            Filename = file.FileName,
                            ContentType = file.ContentType,
                            FileContent = fileContent
                        };
                        review.ReviewAttachments.Add(attachment);
                    }
                }
                
                this.dataContext.Reviews.Add(review);
                this.dataContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = this.dataContext.Reviews.Find(id.Value);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")]Review review)
        {
            if (ModelState.IsValid)
            {
                this.dataContext.Entry(review).State = System.Data.Entity.EntityState.Modified;
                this.dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = this.dataContext.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = this.dataContext.Reviews.Find(id.Value);
            if (review == null)
            {
                return HttpNotFound();
            }
            this.dataContext.Reviews.Remove(review);
            this.dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}