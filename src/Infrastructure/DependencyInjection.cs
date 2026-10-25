using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkOMat.Application.Common.Interfaces;
using WorkOMat.Application.Common.Repositories;
using WorkOMat.Infrastructure.Identity;
using WorkOMat.Infrastructure.Implementations;
using WorkOMat.Infrastructure.Persistance;
using WorkOMat.Infrastructure.Persistance.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(o =>
        {
            o.UseNpgsql(configuration.GetConnectionString("DomainDatabase"));
        });
        services.AddDbContext<IdentityDbContext>(o =>
        {
            o.UseNpgsql(configuration.GetConnectionString("IdentityDatabase"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>(o =>
        {
            o.User.RequireUniqueEmail = true;


            o.Password.RequiredLength = 12;
            o.Password.RequireDigit = true;
            o.Password.RequireLowercase = true;
            o.Password.RequireUppercase = true;
            o.Password.RequireNonAlphanumeric = true;


            o.Lockout.MaxFailedAccessAttempts = 5;
            o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

        })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();
            
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped(typeof(Repository<,>));
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        return services;
    }
}