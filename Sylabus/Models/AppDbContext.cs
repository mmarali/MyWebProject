using Microsoft.EntityFrameworkCore;
namespace Sylabus.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Academy> Academies { get; set; }
}