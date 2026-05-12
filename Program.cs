using LearnSecureAPI.Data;
using LearnSecureAPI.Mapper;
using LearnSecureAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDataContext>(options => 

    options.UseSqlServer("Server=localhost,1433;Database=LearnSecureAPI;User Id=sa;Password=*123456fE*;TrustServerCertificate=True;")
    );
builder.Services.RegisterMapps();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Deve ser nessa ordem (Se inverter vai quebrar)

// O sistema deve Autenticar - Saber quem é o usuário - UseAuthentication
// Após saber quem é o sistema as permissões desse usuário no sistema - UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


