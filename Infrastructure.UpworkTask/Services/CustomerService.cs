using Application.UpworkTask.Dtos;
using Application.UpworkTask.Interfaces;
using Domain.UpworkTask.Entities;
using Microsoft.EntityFrameworkCore;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> AddCustomerAsync(CreateCustomerDto customer)
    {
        var model = new Customer { Name = customer.Name };
        _context.Customers.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.Images)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.Include(c => c.Images).ToListAsync();
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}
