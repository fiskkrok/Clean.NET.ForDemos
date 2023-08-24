using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROJECT_DIRECTORY;
using PROJECT_DIRECTORY.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PROJECT_DIRECTORYIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'PROJECT_DIRECTORYIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<PROJECT_DIRECTORYIdentityDbContext>(options => options.UseSqlServer(connectionString));
// Add this line "(IdentityConfiguration.For(builder).Configure)" Ps only in Development mode
builder.Services.AddDefaultIdentity<IdentityUser>(IdentityConfiguration.For(builder).Configure).AddEntityFrameworkStores<PROJECT_DIRECTORYIdentityDbContext>();

// Add services to the container.
// Added options.Conventions.AuthorizePage("/Index") to require authorization for the Index page
builder.Services.AddRazorPages(options => options.Conventions.AuthorizePage("/Index")); // <--- Add this line

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
