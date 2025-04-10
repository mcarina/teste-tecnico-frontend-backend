using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using User.Data;
using User.Routes;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"] ?? "super-secret-key";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// builder.Services.AddSwaggerGen(options =>
// {
//     // 👇 Esta linha define a versão do OpenAPI e corrige o erro
//     options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//     {
//         Title = "User API",
//         Version = "v1"
//     });

//     options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//     {
//         Name = "Authorization",
//         Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//         Scheme = "Bearer",
//         BearerFormat = "JWT",
//         In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//         Description = "Informe: Bearer {seu token JWT}"
//     });

//     options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//     {
//         {
//             new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//             {
//                 Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                 {
//                     Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                     Id = "Bearer"
//                 }
//             },
//             Array.Empty<string>()
//         }
//     });
// });

// Configuração do banco de dados usando o UserContext
builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Registrando as rotas
app.MapUserRoutes();
app.MapLoginRoute();

app.Run();
