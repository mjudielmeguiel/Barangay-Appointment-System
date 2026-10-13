using Barangay_Service_Appointment_System.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

// ✅ Write any hard crash to the console AND crash.log before the process exits
AppDomain.CurrentDomain.UnhandledException += (s, e) =>
{
    var msg = $"{DateTime.Now:u} UNHANDLED: {e.ExceptionObject}";
    Console.Error.WriteLine(msg);
    try { File.AppendAllText("crash.log", msg + Environment.NewLine); } catch { }
};

TaskScheduler.UnobservedTaskException += (s, e) =>
{
    var msg = $"{DateTime.Now:u} UNOBSERVED TASK: {e.Exception}";
    Console.Error.WriteLine(msg);
    try { File.AppendAllText("crash.log", msg + Environment.NewLine); } catch { }
    e.SetObserved();
};

var builder = WebApplication.CreateBuilder(args);

// ✅ 100MB Upload Limit — Form Options
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});

// ✅ 100MB Upload Limit — Kestrel Server
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = 104857600; // 100 MB
});

// --- Mga Serbisyo ---
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ✅ Commented out to prevent the yellow warning in your console during local development
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();