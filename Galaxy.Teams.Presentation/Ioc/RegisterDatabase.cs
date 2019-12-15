using Galaxy.Teams.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Teams.Presentation.Ioc
{
    public static class RegisterDatabase
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeamDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("TeamDb"), x => x.MigrationsAssembly("Galaxy.Teams.Presentation")));
            var build = services.BuildServiceProvider();
            
            var scope = build.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<TeamDbContext>().Database.Migrate();
            return services;
        }
    }
}