using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using SurveySystem.Context;
using SurveySystem.CustomDescriber;
using SurveySystem.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = true; // �ifreler en az bir rakam i�ermelidir.
    options.Password.RequiredLength = 8; // �ifre en az 8 karakter uzunlu�unda olmal�d�r.
    options.Password.RequireLowercase = true; // �ifreler en az bir k���k harf i�ermelidir.
    options.Password.RequireUppercase = true; // �ifreler en az bir b�y�k harf i�ermelidir.
    options.Password.RequireNonAlphanumeric = false; // �ifre en az bir �zel karakter i�ermemelidir.
    options.SignIn.RequireConfirmedEmail = false; // E-posta onay� gerektirme.
    options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon onay� gerektirme.
    options.User.RequireUniqueEmail = true; // E-posta adreslerinin benzersiz olmas�n� sa�lar.

}).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<SurveyContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.Name = "SurveySystemCookie";
    options.ExpireTimeSpan = TimeSpan.FromDays(25); //Oturumun 25 g�n boyunca ge�erli olaca��n� belirtir.
    options.LoginPath = new PathString("/Home/SignIn"); //Kullan�c�n�n oturum a�mas� gerekti�inde y�nlendirilece�i sayfan�n yolu.
});


builder.Services.AddDbContext<SurveyContext>(options =>
options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=SurveySystem;Integrated Security=True"));

var app = builder.Build();


// Admin kullan�c�s�n� ba�latma s�ras�nda olu�tur
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    // UserManager ve RoleManager'� al
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

    // Admin kullan�c�s�n�n e-posta ve �ifresini burada belirtin
    string adminEmail = "admin@example.com";
    string adminPassword = "Admin123";

    // Admin kullan�c�s�n� olu�tur (e�er yoksa)
    var adminUser = userManager.FindByEmailAsync(adminEmail).Result;
    if (adminUser == null)
    {
        adminUser = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FirstName = "Admin", 
            LastName = "Admin"
        };

        // Admin rol�n� olu�tur (e�er yoksa)
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            var adminRole = new AppRole
            {
                Name = "Admin"
            };
            roleManager.CreateAsync(adminRole).Wait();
        }

        // Admin kullan�c�s�n� olu�tur
        var result = userManager.CreateAsync(adminUser, adminPassword).Result;

        // Admin kullan�c�s�n� "Admin" rol�ne ata
        if (result.Succeeded)
        {
            userManager.AddToRoleAsync(adminUser, "Admin").Wait();
        }
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions {
//    RequestPath="/node_modules",
//    FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules"))
//});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=DeleteQuestion}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapDefaultControllerRoute();
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "home",
//    pattern: "Home/{action=Create}",
//    defaults: new { controller = "Home", action = "Create" });


app.Run();
