using Domain.Abstract;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});

var connectionString = configuration["ConnectionString:DefaultConnection"];

builder.Services.AddSingleton<EFDbContext>(s => new EFDbContext(connectionString));

builder.Services.AddTransient<IUserRepository, EFUserRepository>();
builder.Services.AddTransient<IFamilyRepository, EFFamilyRepository>();
builder.Services.AddSingleton<ICharacterRepository, EFCharacterRepository>();
builder.Services.AddSingleton<IGoalRepository, EFGoalRepository>();
builder.Services.AddSingleton<IPreferenceRepository, EFPreferenceRepository>();
builder.Services.AddSingleton<ICareerRepository, EFCareerRepository>();
builder.Services.AddSingleton<IInheritanceLawRepository, EFInheritanceLawRepository>();
builder.Services.AddSingleton<Domain.Migrator.Migrator>();


services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseAuthentication();




app.UseEndpoints(builder => builder.MapControllers());

app.MapControllerRoute(
    "Start",
    "",
    new { controller = "Randomizer", action = "Goal" }
);

app.MapControllerRoute(
    "Family",
    "Family/Create",
    new { controller = "Family", action = "Create"}
);

app.MapControllerRoute(
    "GetMarried",
    "Character/GetMarried/{familyId}",
    new { controller = "Character", action = "Create", familyId = @"\d+", type = 2 }
);

app.MapControllerRoute(
    "GIveBirth",
    "Character/GiveBirth/{familyId}",
    new { controller = "Character", action = "Create", familyId = @"\d+", type = 1 }
);

app.MapControllerRoute(
    "CreateCharacter",
    "Character/Create/{familyId}",
    new { controller = "Character", action = "Create", familyId = @"\d+", type = 0 }
);

app.MapControllerRoute(
    "Admin",
    "Admin/",
    new { controller = "Admin", action = "Users" }
);

app.MapControllerRoute(
    "AdminCreateCharacter",
    "Admin/NewCharacter/{familyId}",
    new { controller = "Admin", action = "Character", id = 0, familyId = @"\d+" }
);

app.MapControllerRoute(
    "defaultWithId",
    "{controller}/{action}/{id}",
    new { controller = "Admin", action = "Goal", id = @"\d+" }
);

app.MapControllerRoute(
    "default",
    "{controller}/{action}"
);

app.MapControllerRoute(
    "Family",
    "Family/{familyId}",
    new { controller = "Family", action = "List", familyId = @"\d+" }
);

var migrator = app.Services.GetRequiredService<Domain.Migrator.Migrator>();
migrator.Migrate();

app.Run();