using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Index(
            [FromForm] IList<IFormFile> files,
            [FromQuery] string param1,
            [FromQuery] string param2)
        {
            var dirUploads = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            foreach (IFormFile file in files)
            {
                if (file.Length > 0)
                {
                    try
                    {

                        Directory.CreateDirectory(dirUploads);

                        using FileStream filestream = System.IO.File.Create(Path.Combine(dirUploads, file.FileName));
                        await file.CopyToAsync(filestream);
                        filestream.Flush();
                    }
                    catch (Exception ex)
                    {
                        throw ex.InnerException;
                    }
                }
            }

            return Ok();
        }
    }
}
