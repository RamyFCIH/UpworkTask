using Application.UpworkTask.Interfaces;
using Domain.UpworkTask.Entities;
using Microsoft.EntityFrameworkCore;

public class LeadImageService : ILeadImageService
{
    private readonly AppDbContext _context;

    public LeadImageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> UploadImagesAsync(int leadId, List<string> base64Images)
    {
        var lead = await _context.Leads
            .Include(l => l.Images)
            .FirstOrDefaultAsync(l => l.Id == leadId);

        if (lead == null) throw new Exception("Lead not found.");

        if (lead.Images.Count + base64Images.Count > 10)
        {
            int remaining = 10 - lead.Images.Count;
            throw new Exception($"Limit exceeded. You can only upload {remaining} more images.");
        }

        foreach (var base64 in base64Images)
        {
            lead.Images.Add(new LeadImage { Base64Image = base64 });
        }

        await _context.SaveChangesAsync();
        return "Images uploaded successfully.";
    }

    public async Task<List<(int Id, string Base64Image)>> GetImagesAsync(int leadId)
    {
        return await _context.LeadImages
            .Where(i => i.LeadId == leadId)
            .Select(i => new ValueTuple<int, string>(i.Id, i.Base64Image))
            .ToListAsync();
    }

    public async Task<string> DeleteImageAsync(int imageId)
    {
        var image = await _context.LeadImages.FindAsync(imageId);
        if (image == null) throw new Exception("Image not found.");

        _context.LeadImages.Remove(image);
        await _context.SaveChangesAsync();

        return "Image deleted successfully.";
    }
}
