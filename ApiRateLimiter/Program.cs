using Microsoft.EntityFrameworkCore;
using ApiRateLimiter.Models;
using ApiRateLimiter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add SQLite database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=ApiRateLimiter.db"));

// Register RateLimiterService with dependency injection
builder.Services.AddScoped<RateLimiterService>(provider =>
{
    var context = provider.GetRequiredService<ApplicationDbContext>();
    return new RateLimiterService(context, 5, TimeSpan.FromMinutes(1)); // Set limit to 5 requests per minute
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configure the default route for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RateLimiter}/{action=Index}/{id?}");

app.Run();
