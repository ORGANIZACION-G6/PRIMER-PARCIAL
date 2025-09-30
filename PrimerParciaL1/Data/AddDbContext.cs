using Microsoft.EntityFrameworkCore;
using PrimerParciaL1.Models;

namespace PrimerParciaL1.Data;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}


