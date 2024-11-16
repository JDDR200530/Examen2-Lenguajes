using Examen_2_Lenguajes.Database.Context;
using Examen_2_Lenguajes.Dto.Auth;
using Examen_2_Lenguajes.Dto.Common;
using Examen_2_Lenguajes.Entity;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Examen_2_Lenguajes.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // Method to generate JWT token
        private JwtSecurityToken GenerateToken(UserEntity userEntity)
        {
            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, userEntity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT ID
            new Claim("UserId", userEntity.Id) // Custom claim for User ID
        };

            // Add roles to token if any
            var userRoles = _userManager.GetRolesAsync(userEntity).Result;
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWT:Expires"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return jwtToken;
        }

        // Login method with token generation
        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var userEntity = await _userManager.FindByEmailAsync(dto.Email);
                var jwtToken = GenerateToken(userEntity);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Login successful",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{userEntity.FirstName} {userEntity.LastName}",
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken) // Return JWT token
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 401,
                Status = false,
                Message = "Invalid login attempt"
            };
        }

        // Register method with token generation
        public async Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var user = new UserEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");

                var jwtToken = GenerateToken(user);

                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Registration successful",
                    Data = new LoginResponseDto
                    {
                        FullName = $"{user.FirstName} {user.LastName}",
                        Email = user.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken) // Return JWT token
                    }
                };
            }

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 400,
                Status = false,
                Message = "Error registering user"
            };
        }
    }

}

