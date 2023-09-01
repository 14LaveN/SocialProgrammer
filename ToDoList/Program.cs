using Microsoft.AspNetCore.Authentication.Cookies;
using NLog.Web;
using SocialProgrammer;
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
    options.LoginPath = new PathString("/User/LoginForm");
    options.AccessDeniedPath = new PathString("/User/LoginForm");
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