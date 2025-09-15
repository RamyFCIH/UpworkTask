using Domain.UpworkTask.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerImage> CustomerImages { get; set; }

    public DbSet<Lead> Leads { get; set; }
    public DbSet<LeadImage> LeadImages { get; set; }
}
