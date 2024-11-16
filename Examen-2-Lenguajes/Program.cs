using Examen_2_Lenguajes;
using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app, app.Environment);
using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<PartidasDbContext>();
        var userManager = services.GetRequiredService < UserManager<UserEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await PartidaSeeder.LoadDataAsync(context, loggerFactory, userManager, roleManager);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error al ejecutar el Seed de Data");
    }
}
app.Run();
