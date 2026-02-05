using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wizardsoft_testtask.Dtos;
using wizardsoft_testtask.Models;

namespace wizardsoft_testtask.Service.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse?> Login(LoginRequest request);
        Task<RegisterResponse?> Register(RegisterRequest request);
    }
}
