using DataAccess.DataContext;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("http://localhost:5000");
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PollDbContext>();
builder.Services.AddControllersWithViews();

var repoSetting = builder.Configuration.GetValue<int>("RepositorySetting");

switch (repoSetting)
{
    case 1:
        builder.Services.AddScoped<IPollRepository, PollFileRepository>();
        break;
    case 2:
    default:
        builder.Services.AddScoped<IPollRepository, PollRepository>();
        break;
}

//builder.Services.AddScoped<IPollRepository, PollRepository>();
//builder.Services.AddScoped<IPollRepository, PollFileRepository>();

var app = builder.Build();

app.UseMigrationsEndPoint();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//   
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
