using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Alumni.Domain.Interfaces;
using Alumni.Infrastructure.Data;
using Alumni.Infrastructure.Repositories;
using Alumni.Infrastructure.Services;
using Alumni.Application.Interfaces;

namespace Alumni.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Add Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAlumniRepository, AlumniRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IEventRegistrationRepository, EventRegistrationRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<INavigationRepository, NavigationRepository>();

        // Add Services
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordService, PasswordService>();

        // Configure JWT Settings
        services.Configure<JwtSettings>(configuration.GetSection("JWT"));

        // Configure JWT Authentication
        var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>();
        if (jwtSettings != null)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });
        }

        return services;
    }
}
