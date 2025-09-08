using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Robot; // يحتوي على AppDbContext والنماذج Models

var builder = WebApplication.CreateBuilder(args);

// 🗂️ الاتصال بقاعدة البيانات (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// 🌐 إعداد HttpClient للاتصال بـ API خارجي
builder.Services.AddHttpClient("MyApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
    client.Timeout = TimeSpan.FromSeconds(30);
});

// 🔐 إعداد نظام تسجيل الدخول (Identity)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

// 🧾 دعم Razor Pages
builder.Services.AddRazorPages();

// 🚀 بناء التطبيق
var app = builder.Build();

// ⚙️ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 📍 ربط الصفحات والموجهات
app.MapRazorPages();
app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
