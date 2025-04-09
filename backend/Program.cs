using User.Data;
using Microsoft.EntityFrameworkCore;
using User.Routes;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();
