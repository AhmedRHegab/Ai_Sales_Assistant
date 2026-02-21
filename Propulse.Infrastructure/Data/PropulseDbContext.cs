using Microsoft.EntityFrameworkCore;
using Propulse.Core.Entities;

namespace Propulse.Infrastructure.Data;

public class PropulseDbContext : DbContext
{
    public PropulseDbContext(DbContextOptions<PropulseDbContext> options) : base(options)
    {
    }

    public DbSet<WhatsAppMessage> WhatsAppMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration if needed
    }
}
