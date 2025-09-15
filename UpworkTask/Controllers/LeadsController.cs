using Application.UpworkTask.Dtos;
using Application.UpworkTask.Interfaces;
using Domain.UpworkTask.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly ILeadService _service;

    public LeadsController(ILeadService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddLead([FromBody] CreateLeadDto lead)
    {
        var created = await _service.AddLeadAsync(lead);
        return CreatedAtAction(nameof(GetLeadById), new { id = created.Id }, created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLeadById(int id)
    {
        var lead = await _service.GetLeadByIdAsync(id);
        if (lead == null) return NotFound();
        return Ok(lead);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLeads()
    {
        var leads = await _service.GetAllLeadsAsync();
        return Ok(leads);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLead(int id)
    {
        var success = await _service.DeleteLeadAsync(id);
        return success ? NoContent() : NotFound();
    }
}
