using Microsoft.AspNetCore.Mvc;
namespace FileUpload.Controllers;

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
        Directory.CreateDirectory(dirUploads);

        foreach (IFormFile file in files)
        {
            if (file.Length <= 0)
                continue;

            try
            {
                using FileStream filestream = System.IO.File.Create(Path.Combine(dirUploads, file.FileName));
                await file.CopyToAsync(filestream);
                filestream.Flush();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        return Ok();
    }
}
