namespace User.Routes
{
    using User.Models;
    using User.Models.Requests;
    using User.Data;
    using Microsoft.EntityFrameworkCore;

    public static class UserRoute
    {
        public static void MapUserRoutes(this WebApplication app)
        {
            var route = app.MapGroup("user");

            route.MapGet("", async (UserContext context) =>
            {
                var users = await context.Users.ToListAsync();
                return Results.Ok(users);
            });


            route.MapPost("", async (UserRequest req, UserContext context) =>
            {
                var user = new UserModel( req.Name, req.Email, req.Password ); 
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Results.Created($"/user/{user.Id}", user);
            });

        }
    }
}
