using ReviewNotes.Infrastructure;
using ReviewNotes.WebUI.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReviewNotes.WebUI.Controllers
{

    //http://www.mikesdotnetting.com/article/259/asp-net-mvc-5-with-ef-6-working-with-files
    //http://techbrij.com/crud-file-upload-asp-net-mvc-ef-multiple
    //http://cpratt.co/file-uploads-in-asp-net-mvc-with-view-models/

    public class FilesController : Controller
    {
        private DataContext context = new DataContext();

        // GET: Files
        public ActionResult Index()
        {
            var files = context.Attachments;
            // list of files
            return View(files);
        }

        public ActionResult FileSystemUpload1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileSystemUpload1(HttpPostedFileBase file)
        {
            //FileSystemUploader.UploadFile(file);
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
            file.SaveAs(path);

            //Post-Redirect
            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult DatabaseUpload1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DatabaseUpload1(HttpPostedFileBase file)
        {
            string fileName = file.FileName;
            
            using (var reader = new System.IO.BinaryReader(file.InputStream))
            {
                byte[] fileContent = reader.ReadBytes(file.ContentLength);
                this.context.Attachments.Add(new Core.ReviewAttachment { FileContent = fileContent, Filename = file.FileName, ContentType = file.ContentType });
                this.context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        
        public ActionResult GetFile(int id)
        {
            // TO DO: add prompt functionality, ask open or save after clicking the link
            var file = this.context.Attachments.Find(id);
            return File(file.FileContent, file.ContentType, file.Filename);
        }
    }
}