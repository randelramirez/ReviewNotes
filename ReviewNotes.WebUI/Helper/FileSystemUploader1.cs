using System.IO;
using System.Web;

namespace ReviewNotes.WebUI.Helper
{
    public static class FileSystemUploader1
    {
        private const string UPLOAD_DIRECTORY = "~/Uploads/";

        public static string UploadFile(HttpPostedFileBase file)
        {
            var path = string.Empty;
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(UPLOAD_DIRECTORY), fileName);
                file.SaveAs(path);
            }
            return path;
        }
        public static void DeleteFile(string fileName)
        {
            // Don't do anything if there is no name
            if (fileName.Length == 0) return;
            // Set our full path for deleting
            string path = UPLOAD_DIRECTORY + "/" + fileName;
            // Check if our file exists
            if (File.Exists(Path.GetFullPath(path)))
            {
                // Delete our file
                File.Delete(Path.GetFullPath(path));
            }
        }
    }
}

