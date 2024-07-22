using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Composite.DP.Models;

public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Book> Books { get; set; }

    public DbSet<Category> Categories { get;}
}