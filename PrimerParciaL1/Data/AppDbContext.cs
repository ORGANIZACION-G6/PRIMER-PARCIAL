using Microsoft.EntityFrameworkCore;
using PrimerParciaL1.Models;

namespace PrimerParcialProgra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>();
}