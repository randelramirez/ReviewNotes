using System.Collections.Generic;
using System.Web;

namespace ReviewNotes.WebUI.Helper
{
    public static class HttpPostedFileHelper
    {
        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            byte[] fileContent = null;
            using (var reader = new System.IO.BinaryReader(file.InputStream))
            {
                fileContent = reader.ReadBytes(file.ContentLength);
            }

            return fileContent;
        }

        public static List<byte[]> ToByteArray(this IEnumerable<HttpPostedFileBase> files)
        {
            var result = new List<byte[]>();
            foreach (var file in files)
            {
                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    byte[] bytes = reader.ReadBytes(file.ContentLength);
                    result.Add(bytes);
                }
            }
            return result;
        }
    }
}
