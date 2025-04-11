namespace User.Routes
{
    using User.Models;
    using User.Models.Requests;
    using User.Data;
    using Login.Models.Requests;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;


    public static class UserRoute
    {
        public static void MapUserRoutes(this WebApplication app)
        {
            var route = app.MapGroup("user");

            route.MapGet("", [Authorize] async (UserContext context) =>
            {
                var users = await context.Users.ToListAsync();
                return Results.Ok(users);
            });

            route.MapGet("/{id:guid}", [Authorize] async (Guid id, UserContext context) =>
            {
                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound($"User with id {id} not found.");
                }

                return Results.Ok(user);
            });

            route.MapPost("", async (UserRequest req, UserContext context) =>
            {
                var tempUser = new UserModel(req.Name, req.Email, ""); // senha vazia só pra criar o objeto
                var hasher = new PasswordHasher<UserModel>();
                var hashedPassword = hasher.HashPassword(tempUser, req.Password);

                var user = new UserModel(req.Name, req.Email, hashedPassword);

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Results.Created($"/user/{user.Id}", new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.CreatedAt,
                    user.UpdatedAt
                });
            });

            route.MapPut("/{id:guid}", [Authorize] async (Guid id, UserRequest req, UserContext context) =>
            {
                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound($"User with id {id} not found.");
                }

                // Atualiza apenas se um novo valor for fornecido
                if (!string.IsNullOrEmpty(req.Name))
                {
                    user.Name = req.Name;
                }

                if (!string.IsNullOrEmpty(req.Email))
                {
                    user.Email = req.Email;
                }

                if (!string.IsNullOrEmpty(req.Password))
                {
                    var hasher = new PasswordHasher<UserModel>();
                    user.Password = hasher.HashPassword(user, req.Password);
                }

                // Atualizar o UpdatedAt apenas se algum campo foi alterado
                if (!string.IsNullOrEmpty(req.Name) || !string.IsNullOrEmpty(req.Email) || !string.IsNullOrEmpty(req.Password))
                {
                    user.UpdatedAt = DateTime.UtcNow;
                }

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Results.Ok(user);
            });


            route.MapDelete("/{id:guid}", [Authorize] async (Guid id, UserContext context) =>
            {
                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound($"User with id {id} not found.");
                }

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return Results.NoContent(); // 204 No Content
            });

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

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyUltraSecureKeyThatIsAtLeast32Chars!!")); // use a mesma chave do Program.cs
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
