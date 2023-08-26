using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NLog.Web;
using SocialProgrammer;
using SocialProgrammer.DAL;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.MongoSettings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("MongoConnection"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/User/LoginForm");
    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/User/LoginForm");
});

builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);
builder.Logging.AddNLogWeb("nlog.config");

builder.Host.UseNLog();

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Profile}/{action=ProfileForm}/{id?}");

app.Run();