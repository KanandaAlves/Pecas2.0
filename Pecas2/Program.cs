using Pecas2.Data;
using Microsoft.EntityFrameworkCore;
using Pecas2.Services;
using Pecas2.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<CurrencyController>(); // Registra o HttpClient
builder.Services.AddSingleton<ApiService>(); // Registra o ApiService como um serviço

var connectionString = builder.Configuration.GetConnectionString("PecasDbConnection");
builder.Services.AddDbContext<PecasContext>(options => options.UseSqlServer(connectionString));



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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();