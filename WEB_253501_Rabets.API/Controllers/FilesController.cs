using Microsoft.AspNetCore.Mvc;

namespace WEB_253501_Rabets.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly string _imagePath;

    public FilesController(IWebHostEnvironment webHost)
    {
        _imagePath = Path.Combine(webHost.WebRootPath, "images");
    }

    [HttpPost]
    public async Task<IActionResult> SaveFile(IFormFile file)
    {
        if (file is null)
        {
            return BadRequest();
        }
        var filePath = Path.Combine(_imagePath, file.FileName);
        var fileInfo = new FileInfo(filePath);

        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        using var fileStream = fileInfo.Create();
        await file.CopyToAsync(fileStream);

        var host = HttpContext.Request.Host;
        var fileUrl = $"Https://{host}/images/{file.FileName}";

        return Ok(fileUrl);
    }

    [HttpDelete]
    public IActionResult DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_imagePath, fileName);
        var fileInfo = new FileInfo(filePath);

        if (fileInfo.Exists)
        {
            fileInfo.Delete();
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }
}
