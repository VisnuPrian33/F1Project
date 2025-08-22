using F1Project.Interface;
using F1Project.Models;
using F1Project.Repository;
using F1Project.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<F1projectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));
builder.Services.AddScoped<IF1team, F1teamRepository>();
builder.Services.AddScoped<F1teamService>();
builder.Services.AddScoped<IF1driver, F1driverRepository>();
builder.Services.AddScoped<F1driverService>();
builder.Services.AddScoped<IWorldchampion, WorldchampionRepository>();
builder.Services.AddScoped<WorldchampionService>();
builder.Services.AddScoped<IUser, UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{

    options.TokenValidationParameters = new

    TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new

    SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]!)),
        ValidateIssuer = false,
        ValidateAudience = false

    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
{
new OpenApiSecurityScheme
{
Reference = new OpenApiReference
{
Type = ReferenceType.SecurityScheme,
Id = "Bearer"
}
},
Array.Empty<string>()
}
});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
