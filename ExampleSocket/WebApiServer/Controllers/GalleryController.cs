using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using WebApiServer.Helpers;

namespace WebApiServer.Controllers
{
    public class UploadImage
    {
        public string Photo { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadImage([FromBody] UploadImage model)
        {
            try
            {
                string nameFile = Guid.NewGuid().ToString()+".jpg";
                var img = model.Photo.FromBase64ToImage();
                if(img == null) { throw new Exception(); }
                var dir=Path.Combine(Directory.GetCurrentDirectory(), "uploads", nameFile);
                img.Save(dir, ImageFormat.Jpeg);
                return Ok(new { image = $"/images/{nameFile}" });
            }
            catch 
            {
                return BadRequest(new { error = "Помилка збереження фото" });
            }
        }
    }

}
