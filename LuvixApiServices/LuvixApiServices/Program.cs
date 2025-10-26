using FluentValidation;
using LuvixApiServices.Context;
using LuvixApiServices.CustomProperties;
using LuvixApiServices.Models;
using LuvixApiServices.Models.DTOs.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

// Para las validaciones con FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ValidacionesUsuario>();

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});


//builder.Services.AddSingleton<Utilidades>();
builder.Services.AddScoped<Utilidades>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,  
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
    };
}); 


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader());
    //options.AddPolicy("AllowAll", policy =>
    //{
    //    policy.AllowAnyOrigin()
    //          .AllowAnyHeader()
    //          .AllowAnyMethod();
    //});
});

// Cors
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        policy => policy.WithOrigins(origenesPermitidos)
//                       .AllowAnyMethod()
//                       .AllowAnyHeader());
//});

var app = builder.Build();
app.UseCors("AllowAngularApp");
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.Path = "/openapi";
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
