using BookingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TreatmentApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Customer> Customers { get; set;}
    public DbSet<Treatment> Treatments { get; set;}
    public DbSet<TimeInterval> TimeIntervals { get; set;}
}
