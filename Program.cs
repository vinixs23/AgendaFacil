using System.Data.SqlClient;
using AgendaFacil.Repositorys;
using AgendaFacil.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Repositórios e serviços
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new AgendamentoRepository(connectionString));
// builder.Services.AddSingleton(new EventoRepository(connectionString));
// builder.Services.AddSingleton(new HorarioRepository(connectionString));
// builder.Services.AddSingleton(new AdminRepository(connectionString));

builder.Services.AddSingleton<AgendamentoService>();

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

app.MapControllers();

app.Run();