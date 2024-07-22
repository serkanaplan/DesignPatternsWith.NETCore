using WebApp.Decorator.DP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Decorator.DP.Repositories;
using Microsoft.Extensions.Caching.Memory;
using WebApp.Decorator.DP.Repositories.Decorator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

    var context = sp.GetRequiredService<AppIdentityDbContext>();
    var memoryCache = sp.GetRequiredService<IMemoryCache>();
    var productRepository = new ProductRepository(context);
    var logService = sp.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();

    if (httpContextAccessor.HttpContext.User.Identity.Name == "user1")
    {
        var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
        return cacheDecorator;
    }

    var logDecorator = new ProductRepositoryLoggingDecorator(productRepository, logService);

    return logDecorator;
});

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddControllersWithViews();



var app = builder.Build();

//migrations işlemini otomatik hale getirmek ve seed data eklemek için... 
using (var scope = app.Services.CreateScope())
{
    var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    identityDbContext.Database.Migrate();

    if (!userManager.Users.Any())
    {
        userManager.CreateAsync(new AppUser() { UserName = "user1", Email = "user1@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "user2", Email = "user2@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "user3", Email = "user3@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "user4", Email = "user4@outlook.com" }, "Password12*").Wait();
        userManager.CreateAsync(new AppUser() { UserName = "user5", Email = "user5@outlook.com" }, "Password12*").Wait();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
