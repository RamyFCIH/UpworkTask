using Application.UpworkTask.Interfaces;
using Domain.UpworkTask.Entities;
using Microsoft.EntityFrameworkCore;
using System;

public class CustomerImageService : ICustomerImageService
{
    private readonly AppDbContext _context;

    public CustomerImageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> UploadImagesAsync(int customerId, List<string> base64Images)
    {
        var customer = await _context.Customers
            .Include(c => c.Images)
            .FirstOrDefaultAsync(c => c.Id == customerId);

        if (customer == null) throw new Exception("Customer not found.");

        if (customer.Images.Count + base64Images.Count > 10)
        {
            int remaining = 10 - customer.Images.Count;
            throw new Exception($"Limit exceeded. You can only upload {remaining} more images.");
        }

        foreach (var base64 in base64Images)
        {
            customer.Images.Add(new CustomerImage { Base64Image = base64 });
        }

        await _context.SaveChangesAsync();
        return "Images uploaded successfully.";
    }

    public async Task<List<(int Id, string Base64Image)>> GetImagesAsync(int customerId)
    {
        return await _context.CustomerImages
            .Where(i => i.CustomerId == customerId)
            .Select(i => new ValueTuple<int, string>(i.Id, i.Base64Image))
            .ToListAsync();
    }

    public async Task<string> DeleteImageAsync(int imageId)
    {
        var image = await _context.CustomerImages.FindAsync(imageId);
        if (image == null) throw new Exception("Image not found.");

        _context.CustomerImages.Remove(image);
        await _context.SaveChangesAsync();

        return "Image deleted successfully.";
    }
}
