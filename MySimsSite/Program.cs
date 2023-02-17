using Domain.Abstract;
using Domain.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using MjauriziaSims.MessageManager;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

var connectionString = configuration["ConnectionString:DefaultConnection"];

builder.Services.AddSingleton<EFDbContext>(s => new EFDbContext(connectionString));
builder.Services.AddTransient<IUserRepository, EFUserRepository>();
builder.Services.AddTransient<IMsgRepository, EFMsgRepository>();
builder.Services.AddTransient<IFamilyRepository, EFFamilyRepository>();
builder.Services.AddSingleton<ICharacterRepository, EFCharacterRepository>();
builder.Services.AddSingleton<IGoalRepository, EFGoalRepository>();
builder.Services.AddSingleton<IPreferenceRepository, EFPreferenceRepository>();
builder.Services.AddSingleton<ICareerRepository, EFCareerRepository>();
builder.Services.AddSingleton<IInheritanceLawRepository, EFInheritanceLawRepository>();
services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<Domain.Migrator.Migrator>();
builder.Services.AddSingleton<MessageManager>();
builder.Services.AddSingleton<IHtmlLocalizerFactory, HtmlLocalizerFactory>();
builder.Services.AddSingleton<IViewLocalizer, ViewLocalizer>();

services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ru"),
    };
    options.DefaultRequestCulture = new RequestCulture("ru");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
        options.AccessDeniedPath = "/";
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        googleOptions.CallbackPath = "/signin-google";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();
services.AddLocalization(options => options.ResourcesPath = "Resources");

services.AddMvc()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

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

var supportedCultures = new[] { "en", "ru" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.UseAuthentication();
app.UseAuthorization();

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
//migrator.Migrate();

app.Run();