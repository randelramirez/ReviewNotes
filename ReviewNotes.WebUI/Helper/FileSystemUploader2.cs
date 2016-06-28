using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ReviewNotes.WebUI.Helper
{
    public static class FileSystemUploader2
    {
        public static char DirSeparator = System.IO.Path.DirectorySeparatorChar;
        public static string FilesPath = "Content" + DirSeparator + "Uploads" + DirSeparator;

        public static string UploadFile(HttpPostedFileBase file)
        {
            // Check if we have a file
            if (null == file) return "";
            // Make sure the file has content
            if (!(file.ContentLength > 0)) return "";
            string fileName = file.FileName;
            string fileExt = Path.GetExtension(file.FileName);
            // Make sure we were able to determine a proper
            // extension
            if (null == fileExt) return "";
            // Check if the directory we are saving to exists
            if (!Directory.Exists(FilesPath))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(FilesPath);
            }
            // Set our full path for saving
            string path = FilesPath + DirSeparator + fileName;
            // Save our file
            file.SaveAs(Path.GetFullPath(path));
            // Return the filename
            return fileName;
        }
        public static void DeleteFile(string fileName)
        {
            // Don't do anything if there is no name
            if (fileName.Length == 0) return;
            // Set our full path for deleting
            string path = FilesPath + DirSeparator + fileName;
            // Check if our file exists
            if (File.Exists(Path.GetFullPath(path)))
            {
                // Delete our file
                File.Delete(Path.GetFullPath(path));
            }
        }
    }
}
