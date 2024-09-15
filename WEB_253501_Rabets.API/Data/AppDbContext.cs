using Microsoft.EntityFrameworkCore;
using WEB_253501_Rabets.Domain.Entities;

namespace WEB_253501_Rabets.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    public DbSet<ElectricProduct> ElectricProducts { get; set; }
    public DbSet<Category> Categories { get; set; }

}
