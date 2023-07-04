using System.Drawing;

namespace WebApiServer.Helpers
{
    public static class ImageWorker
    {
        public static Bitmap FromBase64ToImage(this string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            try
            {
                using(MemoryStream ms = new MemoryStream(bytes))
                {
                    ms.Position = 0;
                    Image image = Image.FromStream(ms);
                    ms.Close();
                    return new Bitmap(image);
                }
            }
            catch { return null; }
        }
    }
}
