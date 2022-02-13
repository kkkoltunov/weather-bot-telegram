using Microsoft.EntityFrameworkCore;
using WeatherBot.DAL.Entities;

namespace WeatherBot.DAL;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}