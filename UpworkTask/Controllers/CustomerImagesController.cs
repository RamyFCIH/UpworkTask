using Application.UpworkTask.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomerImagesController : ControllerBase
{
    private readonly ICustomerImageService _service;

    public CustomerImagesController(ICustomerImageService service)
    {
        _service = service;
    }

    [HttpPost("{customerId}/upload")]
    public async Task<IActionResult> UploadImages(int customerId, [FromBody] List<string> base64Images)
    {
        try
        {
            var message = await _service.UploadImagesAsync(customerId, base64Images);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetImages(int customerId)
    {
        var images = await _service.GetImagesAsync(customerId);
        return Ok(images);
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
