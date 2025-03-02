using System.Reflection;

namespace Cinemate.Models
{
    public class ImageConverter
    {
        //public static string ImageToBase64(Stream imageStream)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        imageStream.CopyTo(memoryStream);
        //        byte[] imageBytes = memoryStream.ToArray();
        //        string base64String = Convert.ToBase64String(imageBytes);
        //        return base64String;
        //    }
        //}

        public static string ImageToBase64(FileResult fileResult)
        {
            using (Stream imageStream = fileResult.OpenReadAsync().Result)
            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static ImageSource Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Stream stream = new MemoryStream(imageBytes);
            return ImageSource.FromStream(() => stream);
        }

        public static string ConvertImageToBase64(string embeddedResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(embeddedResourceName))
            {
                if (stream == null) throw new InvalidOperationException("Resource not found.");
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
    }
}
