using Microsoft.EntityFrameworkCore;
using NetCore_01;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//il framework creerà un'istanza di CustomConsoleLogger alle classi che richiedono una dipendenza di tipo ICustomLogger
builder.Services.AddScoped<ICustomLogger, CustomConsoleLogger>();

builder.Services.AddDbContext<PostContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("BlogConnection"))
    );


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PostContext>();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

//serve per evitare l'eccezione "System.Text.Json.JsonException: A possible object cycle was detected"
//quando si caricano le entities collegate
builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
