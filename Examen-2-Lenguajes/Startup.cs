using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Helpers;
using Examen_2_Lenguajes.Services;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Examen_2_Lenguajes
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHttpContextAccessor();

            var name = configuration.GetConnectionString("DefaultConnection");

            // DbContext Configuration
            services.AddDbContext<PartidasDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<LoginDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LogginsConnection")));

            // Service Configuration
            services.AddTransient<IPartidaService, PartidaService>();
            services.AddTransient<IAuditServices, AuditService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICuentaContableService, CuentaContableService>();

            // Identity Configuration
            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<PartidasDbContext>()
            .AddDefaultTokenProviders();

            // JWT Authentication Configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });

            // AutoMapper Configuration
            services.AddAutoMapper(typeof(AutoMapperProfile));

            // CORS Configuration
            services.AddCors(opt =>
            {
                var allowURLS = configuration.GetSection("AllowURLS").Get<string[]>();

                opt.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins(allowURLS)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            // Add Authentication and Authorization Middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }



}
