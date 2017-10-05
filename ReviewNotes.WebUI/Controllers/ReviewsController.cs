using ReviewNotes.Core;
using ReviewNotes.Infrastructure;
using ReviewNotes.WebUI.Services;
using ReviewNotes.WebUI.ViewModels;
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
        public ActionResult Index(string search)
        {
            ViewBag.Search = search;
            IEnumerable<ReviewViewModel> model;
            if (!string.IsNullOrEmpty(search))
            {
                model = service.GetAllReviews(r => r.Title.Contains(search) || r.Content.Contains(search)).Select(r => new ReviewViewModel { Id = r.Id, Content = r.Content, Title = r.Title, Attachments = r.Attachments });
            }
            else
            {
                model = service.GetAllReviews().Select(r => new ReviewViewModel { Id = r.Id, Content = r.Content, Title = r.Title, Attachments = r.Attachments });
            }
            
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewViewModel model, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                var review = new Review { Title = model.Title, Content = model.Content };
                if (files.Count() > 0 && files.ToList()[0] != null)
                {
                    review.Attachments = ReviewService.UploadedFilesToReviewAttachment(files);
                }
                
                this.service.Add(review);
                this.service.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = this.service.GetOne(id.Value);
            var model = new ReviewViewModel { Id = review.Id, Title = review.Title, Content = review.Content, Attachments = review.Attachments };
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReviewViewModel model, IEnumerable<HttpPostedFileBase> files,
            IEnumerable<int> deletedAttachments)
        {
            if (ModelState.IsValid)
            {
                var review = new Review { Id = model.Id, Title = model.Title, Content = model.Content };
                this.service.Update(review, files, deletedAttachments);
                this.service.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = this.service.GetOne(id.Value);
            var model = new ReviewViewModel { Id = review.Id, Title = review.Title, Content = review.Content, Attachments = review.Attachments };
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(model);
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