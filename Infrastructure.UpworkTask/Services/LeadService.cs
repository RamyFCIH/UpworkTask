using Application.UpworkTask.Dtos;
using Application.UpworkTask.Interfaces;
using Domain.UpworkTask.Entities;
using Microsoft.EntityFrameworkCore;

public class LeadService : ILeadService
{
    private readonly AppDbContext _context;

    public LeadService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Lead> AddLeadAsync(CreateLeadDto leadModel)
    {
        var model = new Lead {Name = leadModel.Name };
        _context.Leads.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<Lead?> GetLeadByIdAsync(int id)
    {
        return await _context.Leads
            .Include(l => l.Images)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<Lead>> GetAllLeadsAsync()
    {
        return await _context.Leads.Include(l => l.Images).ToListAsync();
    }

    public async Task<bool> DeleteLeadAsync(int id)
    {
        var lead = await _context.Leads.FindAsync(id);
        if (lead == null) return false;

        _context.Leads.Remove(lead);
        await _context.SaveChangesAsync();
        return true;
    }
}
