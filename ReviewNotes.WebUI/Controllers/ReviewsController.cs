using ReviewNotes.Core;
using ReviewNotes.Infrastructure;
using ReviewNotes.WebUI.Helper;
using ReviewNotes.WebUI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ReviewNotes.WebUI.Controllers
{
    public class ReviewsController : Controller
    {
        private ReviewService service = new ReviewService(new DataContext());

        // GET: Reviews
        public ActionResult Index()
        {
            return View(service.GetAllReviews());
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
                    review.ReviewAttachments = ReviewService.UploadedFilesToReviewAttachment(files);
                }

                this.service.Add(review);
                this.service.SaveChanges();
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
            Review review = this.service.GetOne(id.Value);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")]Review postedReview, IEnumerable<HttpPostedFileBase> files, 
            IEnumerable<int> deletedAttachments)
        {
            if (ModelState.IsValid)
            {
                this.service.Update(postedReview, files, deletedAttachments);
                this.service.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postedReview);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = this.service.GetOne(id.Value);
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
            Review review = this.service.GetOne(id.Value);
            if (review == null)
            {
                return HttpNotFound();
            }
            this.service.Delete(review);
            this.service.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}