using Examen_2_Lenguajes.Constants;
using Examen_2_Lenguajes.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace Examen_2_Lenguajes.Database.Context
{
    public class PartidaSeeder
    {
        public static async Task LoadDataAsync(
            PartidasDbContext context,
            ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            try
            {
                await LoadRolesAndUserAsync(userManager, roleManager, loggerFactory);
                await LoadCuentasAsync(loggerFactory, context);
                await LoadPartidasAsync(loggerFactory, context);
                await LoadSaldoAsync(loggerFactory, context);
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error initializing the data for the API");
            }
        }

        // Seed roles and user logic
        public static async Task LoadRolesAndUserAsync(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            try
            {
                // Create USER role if it does not exist
                if (!await roleManager.RoleExistsAsync(RolesConstantcs.USER))
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstantcs.USER));
                }

                // Create user if no users exist
                if (!await userManager.Users.AnyAsync())
                {
                    var normalUser = new UserEntity
                    {
                        FirstName = "User",
                        LastName = "Partidas",
                        Email = "partidas@partidas.edu",
                        UserName = "partidas@partidas.edu", // Ensure username matches email
                    };

                    var result = await userManager.CreateAsync(normalUser, "Temporal01*");
                    if (!result.Succeeded)
                    {
                        var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                        logger.LogError("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                    await userManager.AddToRoleAsync(normalUser, RolesConstantcs.USER);
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error loading roles and user.");
            }
        }

        // Seed Partidas data
        public static async Task LoadPartidasAsync(ILoggerFactory loggerFactory, PartidasDbContext context)
        {
            try
            {
                var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "Partidas.json");
                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"The file {jsonFilePath} was not found.");
                }

                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var partidas = JsonConvert.DeserializeObject<List<PartidaEntity>>(jsonContent);

                // Ensure defaults for missing data
                foreach (var partida in partidas)
                {
                    partida.Description = partida.Description ?? "Descripción predeterminada";
                    partida.TipoTransaccion = partida.TipoTransaccion ?? "Debe";
                    partida.UserId = partida.UserId ?? null;
                    partida.NombreCuenta = partida.NombreCuenta ?? null;
                    partida.CodigoCuenta = partida.CodigoCuenta == 0 ? 0 : partida.CodigoCuenta;
                    partida.Monto = partida.Monto == 0 ? 0 : partida.Monto;
                }

                if (!await context.Partidas.AnyAsync())
                {
                    await context.AddRangeAsync(partidas);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error occurred while loading partidas.");
            }
        }

        // Seed Saldo data
        public static async Task LoadSaldoAsync(ILoggerFactory loggerFactory, PartidasDbContext context)
        {
            try
            {
                var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "Saldo.json");
                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"The file {jsonFilePath} was not found.");
                }

                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var saldo = JsonConvert.DeserializeObject<List<SaldoEntity>>(jsonContent);

                // Ensure saldo data is not empty before adding
                if (saldo != null && saldo.Any())
                {
                    context.AddRange(saldo);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error occurred while loading saldo.");
            }
        }

        // Seed CuentasContables data
        public static async Task LoadCuentasAsync(ILoggerFactory loggerFactory, PartidasDbContext context)
        {
            try
            {
                var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SeedData", "CuentasContables.json");
                if (!File.Exists(jsonFilePath))
                {
                    throw new FileNotFoundException($"The file {jsonFilePath} was not found.");
                }

                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var cuentas = JsonConvert.DeserializeObject<List<CuentaContableEntity>>(jsonContent);

                if (cuentas != null && cuentas.Any())
                {
                    context.AddRange(cuentas);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<PartidaSeeder>();
                logger.LogError(e, "Error occurred while loading cuentas.");
            }
        }
    }



}
