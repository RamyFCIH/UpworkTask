using Application.UpworkTask.Dtos;
using Application.UpworkTask.Interfaces;
using Domain.UpworkTask.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto customer)
    {
        var created = await _service.AddCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomerById), new { id = created.Id }, created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerById(int id)
    {
        var customer = await _service.GetCustomerByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        var customers = await _service.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var success = await _service.DeleteCustomerAsync(id);
        return success ? NoContent() : NotFound();
    }
}
