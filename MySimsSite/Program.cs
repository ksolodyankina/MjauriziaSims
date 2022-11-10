using Domain.Abstract;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=DESKTOP-3TT4G7I;Database=(localdb)\\v11.0;User Id=sa;Password=sa;";

builder.Services.AddSingleton<EFDbContext>(s => new EFDbContext(connectionString));

builder.Services.AddSingleton<IFamilyRepository, EFFamilyRepository>();
builder.Services.AddSingleton<ICharacterRepository, EFCharacterRepository>();
builder.Services.AddSingleton<IGoalRepository, EFGoalRepository>();
builder.Services.AddSingleton<IPreferenceRepository, EFPreferenceRepository>();
builder.Services.AddSingleton<ICareerRepository, EFCareerRepository>();

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


app.MapControllerRoute(
    "Start",
    "",
    new { controller = "Family", action = "Index" }
);

app.MapControllerRoute(
    "Family",
    "Family/Create",
    new { controller = "Family", action = "Create"}
);

app.MapControllerRoute(
    "Family",
    "Family/{familyId}",
    new { controller = "Family", action = "List", familyId = @"\d+" }
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
    new { controller = "Admin", action = "Families" }
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


app.Run();