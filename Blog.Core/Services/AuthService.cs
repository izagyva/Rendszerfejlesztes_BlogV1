using Blog.Core.Constans;
using Blog.Core.Models.Users;
using Blog.Data;
using Blog.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;

namespace Blog.Core.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(UserLoginDto loginDto);
        Task<User?> GetUser();
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly BlogMongoDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public AuthService(
            IConfiguration configuration, BlogMongoDbContext context, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<AuthResponseDto> Login(UserLoginDto loginDto)
        {
            var user = await _context.Users.Find(x => x.username.ToLower() == loginDto.username.ToLower()).FirstOrDefaultAsync();
            if (user == null || user.password != loginDto.password)
            {
                throw new Exception("Invalid username or password");
            }

            // Dynamically set the role based on the username
            user.roles.Clear();
            user.SetRoleBasedOnUsername();

            return new AuthResponseDto
            {
                Username = user.username,
                Name = user.name,
                Token = CreateToken(user),
                IsAdmin = user.roles.Contains("Admin")
            };
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.username),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        // Add other claims as needed
    };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Token"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<User?> GetUser()
        {
            var username = _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            return await _context.Users.Find(x => x.username.ToLower() == username.ToLower()).FirstOrDefaultAsync();
        }
    }
}
