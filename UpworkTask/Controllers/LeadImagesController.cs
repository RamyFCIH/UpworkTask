using Application.UpworkTask.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LeadImagesController : ControllerBase
{
    private readonly ILeadImageService _service;

    public LeadImagesController(ILeadImageService service)
    {
        _service = service;
    }

    [HttpPost("{leadId}/upload")]
    public async Task<IActionResult> UploadImages(int leadId, [FromBody] List<string> base64Images)
    {
        try
        {
            var message = await _service.UploadImagesAsync(leadId, base64Images);
            return Ok(message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{leadId}")]
    public async Task<IActionResult> GetImages(int leadId)
    {
        var images = await _service.GetImagesAsync(leadId);
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
