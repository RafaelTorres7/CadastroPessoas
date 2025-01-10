using CadastroPessoas.Data;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Dapper
builder.Services.AddSingleton<Database>();

// Habilitar controllers e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRouting();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pessoas}/{action=Index}/{id?}");

app.Run();
