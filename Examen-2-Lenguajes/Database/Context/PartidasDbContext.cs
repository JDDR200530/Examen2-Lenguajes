
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Examen_2_Lenguajes.Database.Context
{
    public class PartidasDbContext : IdentityDbContext<UserEntity>
    {
        private readonly IAuditServices _auditServices;

        public PartidasDbContext(DbContextOptions<PartidasDbContext> options, IAuditServices auditServices)
            : base(options)
        {
            _auditServices = auditServices;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la colación para cadenas de texto
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            // Asegura que la propiedad UserId en PartidaEntity sea requerida
            modelBuilder.Entity<PartidaEntity>()
                .Property(p => p.UserId)
                .IsRequired();

            // Configura el comportamiento de eliminación de claves foráneas para Restrict
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }
        }

        // DbSets para cada entidad
        public DbSet<PartidaEntity> Partidas { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CuentaContableEntity> CuentaContables { get; set; }
        public DbSet<SaldoEntity> SaldoContable { get; set; }

        // Método para inicializar datos de ejemplo (Seed Data)
        public async Task InitializeSeedDataAsync(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await PartidaSeeder.LoadDataAsync(this, loggerFactory, userManager, roleManager);
        }

        // Sobrescribe SaveChangesAsync para auditar las entidades creadas y modificadas
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null)
                {
                    var userId = _auditServices.GetUserId(); // Obtiene el usuario actual del servicio de auditoría

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = userId;
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.UpdatedBy = userId;
                        entity.UpdatedDate = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }


}

