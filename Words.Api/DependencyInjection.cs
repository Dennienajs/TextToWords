using Microsoft.EntityFrameworkCore;
using Words.Api.Data;
using Words.Api.Services;

namespace Words.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWordsService, WordsService>();

        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, ConfigurationManager builderConfiguration)
    {
        services
            .AddDbContext<WordsDbContext>(options => options.UseSqlServer(builderConfiguration.GetConnectionString("DefaultConnection")))
            .AddHealthChecks()
            .AddDbContextCheck<WordsDbContext>();
        
        return services;
    }
}
