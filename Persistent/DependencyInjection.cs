using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;

namespace Persistent
{
    public static class DependencyInjection
    {
        // добавляет конект БД и регистрирует его
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<NotesDbContext>(options => 
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>());

            return services;
        }
    }
}
