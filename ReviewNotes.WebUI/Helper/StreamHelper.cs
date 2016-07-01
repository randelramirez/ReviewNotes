using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewNotes.WebUI.Helper
{
    public static class StreamHelper
    {
        public static byte[] StreamToByteArray(this Stream stream, int bytes)
        {
            byte[] fileContent = null;
            using (var reader = new System.IO.BinaryReader(stream))
            {
                fileContent = reader.ReadBytes(bytes);
            }

            return fileContent;
        }
    }
}
