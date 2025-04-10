using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using User.Models;
using User.Models.Requests;
using User.Data;

namespace User.Routes
{
    public static class LoginRoute
    {
        public static void MapLoginRoute(this WebApplication app)
        {
            var route = app.MapGroup("/auth");

            route.MapPost("/login", async (LoginRequest req, UserContext context) =>
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
                if (user == null)
                    return Results.Unauthorized();

                var hasher = new PasswordHasher<UserModel>();
                var result = hasher.VerifyHashedPassword(user, user.Password, req.Password);
                if (result != PasswordVerificationResult.Success)
                    return Results.Unauthorized();

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key")); // cuidado para manter em config depois
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Results.Ok(new { token = tokenString });
            });
        }
    }
}
