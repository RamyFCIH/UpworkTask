using Application.UpworkTask.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

[ApiController]
[Route("api/[controller]")]
public class CustomerImagesController : ControllerBase
{
    private readonly ICustomerImageService _service;

    public CustomerImagesController(ICustomerImageService service)
    {
        _service = service;
    }

    [HttpPost("{customerId}/upload-files")]
    public async Task<IActionResult> UploadFiles(int customerId, List<IFormFile> files)
    {
        if (files == null || !files.Any())
            return BadRequest("No files uploaded.");

        var base64Images = new List<string>();

        foreach (var file in files)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            var base64 = Convert.ToBase64String(fileBytes);
            base64Images.Add(base64);
        }

        try
        {
            var message = await _service.UploadImagesAsync(customerId, base64Images);
            return Ok(new { message });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




    [HttpGet("{customerId}/download-all")]
    public async Task<IActionResult> DownloadAllImages(int customerId)
    {
        var images = await _service.GetImagesAsync(customerId);

        if (!images.Any())
            return NotFound("No images found.");

        using var ms = new MemoryStream();
        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
        {
            int index = 1;
            foreach (var img in images)
            {
                var fileBytes = Convert.FromBase64String(img.Base64Image);
                var entry = zip.CreateEntry($"image_{index++}.jpg", CompressionLevel.Fastest);
                using var entryStream = entry.Open();
                entryStream.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        return File(ms.ToArray(), "application/zip", $"customer_{customerId}_images.zip");
    }


    [HttpDelete("delete/{imageId}")]
    public async Task<IActionResult> DeleteImage(int imageId)
    {
        try
        {
            var message = await _service.DeleteImageAsync(imageId);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
