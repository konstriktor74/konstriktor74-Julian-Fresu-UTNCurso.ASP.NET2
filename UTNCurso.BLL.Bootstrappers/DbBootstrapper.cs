using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UTNCurso.DAL.EFCore;

namespace UTNCurso.BLL.Bootstrappers
{
    public static class DbBootstrapper
    {
        public static IServiceCollection SetupDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlite(connectionString ?? throw new InvalidOperationException("Connection string 'TodoContext' not found.")));

            return services;
        }

        public static IdentityBuilder SetupIdentity(this IdentityBuilder builder)
        {
            builder.AddEntityFrameworkStores<TodoContext>();

            return builder;
        }
    }
}