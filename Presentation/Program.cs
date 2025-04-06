using DataAccess.DataContext;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(); // Good for Azure transient faults
    }));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<PollDbContext>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Dependency Injection setup
builder.Services.AddScoped<PollRepository>();
builder.Services.AddScoped<PollFileRepository>();
builder.Services.AddScoped<IPollRepository, DynamicPollRepository>();

var app = builder.Build();

// Exception Handling (show full errors even in production for now)
app.UseDeveloperExceptionPage(); 

if (!app.Environment.IsDevelopment())
{
    // When not in dev, use error handler and HSTS
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Core middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Route mapping
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Simple route to test if the app is alive
app.MapGet("/ping", () => "pong"); 

app.Run();
