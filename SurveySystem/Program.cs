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
    options.Password.RequireDigit = true; // Þifreler en az bir rakam içermelidir.
    options.Password.RequiredLength = 8; // Þifre en az 8 karakter uzunluðunda olmalýdýr.
    options.Password.RequireLowercase = true; // Þifreler en az bir küçük harf içermelidir.
    options.Password.RequireUppercase = true; // Þifreler en az bir büyük harf içermelidir.
    options.Password.RequireNonAlphanumeric = false; // Þifre en az bir özel karakter içermemelidir.
    options.SignIn.RequireConfirmedEmail = false; // E-posta onayý gerektirme.
    options.SignIn.RequireConfirmedPhoneNumber = false; // Telefon onayý gerektirme.
    options.User.RequireUniqueEmail = true; // E-posta adreslerinin benzersiz olmasýný saðlar.

}).AddErrorDescriber<CustomErrorDescriber>().AddEntityFrameworkStores<SurveyContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.Name = "SurveySystemCookie";
    options.ExpireTimeSpan = TimeSpan.FromDays(25); //Oturumun 25 gün boyunca geçerli olacaðýný belirtir.
    options.LoginPath = new PathString("/Home/SignIn"); //Kullanýcýnýn oturum açmasý gerektiðinde yönlendirileceði sayfanýn yolu.
});


builder.Services.AddDbContext<SurveyContext>(options =>
options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=SurveySystem;Integrated Security=True"));

var app = builder.Build();


// Admin kullanýcýsýný baþlatma sýrasýnda oluþtur
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    // UserManager ve RoleManager'ý al
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

    // Admin kullanýcýsýnýn e-posta ve þifresini burada belirtin
    string adminEmail = "admin@example.com";
    string adminPassword = "Admin123";

    // Admin kullanýcýsýný oluþtur (eðer yoksa)
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

        // Admin rolünü oluþtur (eðer yoksa)
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            var adminRole = new AppRole
            {
                Name = "Admin"
            };
            roleManager.CreateAsync(adminRole).Wait();
        }

        // Admin kullanýcýsýný oluþtur
        var result = userManager.CreateAsync(adminUser, adminPassword).Result;

        // Admin kullanýcýsýný "Admin" rolüne ata
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
