using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UTNCurso.Core.Interfaces;
using UTNCurso.Infrastructure;
using UTNCurso.Infrastructure.Repository;

namespace UTNCurso.BLL.Bootstrappers
{
    public static class DbBootstrapper
    {
        public static IServiceCollection SetupDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string 'TodoContext' not found.")));
            services.AddScoped<IAgendaRepository, AgendaRepository>();

            return services;
        }

        public static IdentityBuilder SetupIdentity(this IdentityBuilder builder)
        {
            builder.AddEntityFrameworkStores<TodoContext>();

            return builder;
        }
    }
}