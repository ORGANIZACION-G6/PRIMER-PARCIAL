using Microsoft.EntityFrameworkCore;
using PrimerParcial1.Models;

namespace PrimerParcial1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SupportTicket>().HasIndex(t => t.Status);
        modelBuilder.Entity<SupportTicket>().HasIndex(t => t.Severity);
        modelBuilder.Entity<SupportTicket>().HasIndex(t => t.RequesterEmail);
    }
}