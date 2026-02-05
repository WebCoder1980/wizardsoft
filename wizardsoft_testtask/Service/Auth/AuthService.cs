using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wizardsoft_testtask.Constants;
using wizardsoft_testtask.Data;
using wizardsoft_testtask.Dtos;
using wizardsoft_testtask.Exceptions;
using wizardsoft_testtask.Models;

namespace wizardsoft_testtask.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly AppDbContext _dbContext;

        public AuthService(IOptions<JwtOptions> jwtOptions, AppDbContext dbContext)
        {
            _jwtOptions = jwtOptions.Value;
            _dbContext = dbContext;
        }
        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            User? user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (user == null)
            {
                throw new InvalidCredentialsException();
            }

            string passwordHash = AuthUtil.HashPassword(request.Password);
            if (!string.Equals(user.PasswordHash, passwordHash, StringComparison.Ordinal))
            {
                throw new InvalidCredentialsException();
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_jwtOptions.Key);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            SecurityToken token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresMinutes),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                SigningCredentials = credentials
            });

            string jwt = tokenHandler.WriteToken(token);

            return new LoginResponse(jwt, request.UserName, user.Role);
        }

        public async Task<RegisterResponse?> Register(RegisterRequest request)
        {
            bool exists = await _dbContext.Users.AnyAsync(x => x.UserName == request.UserName);
            if (exists)
            {
                throw new UserAlreadyExistsException();
            }

            User user = new User
            {
                UserName = request.UserName,
                PasswordHash = AuthUtil.HashPassword(request.Password),
                Role = AppRoles.USER
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new RegisterResponse(user.UserName, user.Role);
        }
    }
}
