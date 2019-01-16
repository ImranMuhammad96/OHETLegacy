using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OHET_Project.BLL
{
    public static class ByteFile
    {
        public static byte[] ConvertToBytes(this HttpPostedFileBase image)
        {
            BinaryReader reader = new BinaryReader(image.InputStream);
            byte[] imageBytes = reader.ReadBytes((int)image.ContentLength);

            return imageBytes;
        }
    }
}