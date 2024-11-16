using Examen_2_Lenguajes.Services.Intefaces;

namespace Examen_2_Lenguajes.Services
{
    public class AuditService : IAuditServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var idClaim = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "UserId").FirstOrDefault();
            return idClaim.Value;
          
        }
    }

}
