using System.Reflection;
using Velox.Api.Infrastructure.DAO;
using Velox.Api.Infrastructure.Interface;
using Velox.Api.Middleware.Services.Interfaces;
using Velox.Api.Middleware.Services;
using Velox.Api.Middleware.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Bind JwtSettings from appsettings.json
var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("JwtSettings").Bind(jwtSettings);

// Register JwtSettings in DI container
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddSingleton<DatabaseConfigService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserServiceDAO, UserServiceDAO>();
builder.Services.AddTransient<ITournamentServiceDAO, TournamentServiceDAO>();
builder.Services.AddTransient<ISponserServiceDAO, SponserServiceDAO>();
builder.Services.AddTransient<IPanelistServiceDAO, PanelistServiceDAO>();
builder.Services.AddTransient<IMarqueeServiceDAO, MarqueeServiceDAO>();
builder.Services.AddTransient<ISessionServiceDAO, SessionServiceDAO>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience
        };
    });


// ✅ Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
