using Examen_2_Lenguajes.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examen_2_Lenguajes.Database.Context
{
    public class LoginDbContext : IdentityDbContext<UserEntity>
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options)
        : base(options)
        {
        }

    }
}
