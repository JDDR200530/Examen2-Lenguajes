using Examen_2_Lenguajes.Dto.Auth;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Entity;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Examen_2_Lenguajes.Services.Intefaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto);
    }
}

