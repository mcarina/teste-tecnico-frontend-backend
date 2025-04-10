namespace User.Routes
{
    using User.Models;
    using User.Models.Requests;
    using User.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    public static class UserRoute
    {
        public static void MapUserRoutes(this WebApplication app)
        {
            var route = app.MapGroup("user");

            route.MapGet("", [Microsoft.AspNetCore.Authorization.Authorize] async (UserContext context) =>
            {
                var users = await context.Users.ToListAsync();
                return Results.Ok(users);
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

            route.MapPut("/{id:guid}", [Microsoft.AspNetCore.Authorization.Authorize] async (Guid id, UserRequest req, UserContext context) =>
            {
                var user = await context.Users.FindAsync(id);
                if (user == null)
                {
                    return Results.NotFound($"User with id {id} not found.");
                }

                user.Name = req.Name;
                user.Email = req.Email;
                user.Password = req.Password;
                user.UpdatedAt = DateTime.UtcNow;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Results.Ok(user);
            });

            route.MapDelete("/{id:guid}", [Microsoft.AspNetCore.Authorization.Authorize] async (Guid id, UserContext context) =>
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

        }
    }
}
