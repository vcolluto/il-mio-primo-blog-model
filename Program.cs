using Microsoft.EntityFrameworkCore;
using NetCore_01;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//il framework creerà un'istanza di CustomConsoleLogger alle classi che richiedono una dipendenza di tipo ICustomLogger
builder.Services.AddScoped<ICustomLogger, CustomConsoleLogger>();

builder.Services.AddDbContext<PostContext>(
    options => options.UseSqlServer( builder.Configuration.GetConnectionString("BlogConnection")
       )
    );

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
