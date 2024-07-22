using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Decorator.DP.Models;

public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; }
}