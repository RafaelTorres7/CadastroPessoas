using CadastroPessoas.Data;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão não foi fornecida no arquivo de configuração.");
}

builder.Services.AddSingleton(new Database(connectionString));
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

app.MapControllerRoute(
    name: "enderecos",
    pattern: "{controller=Enderecos}/{action=Index}/{pessoaId?}");

app.Run();
