using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxManagement.Domain.Interfaces;
using TaxManagement.Infrastructure.Database;
using TaxManagement.Infrastructure.Repositories;

namespace TaxManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString,
               npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "core");
            }).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<ITaxRuleRepository, TaxRuleRepository>();
        services.AddScoped<ITaxEntryRepository, TaxEntryRepository>();

        return services;
    }
}

