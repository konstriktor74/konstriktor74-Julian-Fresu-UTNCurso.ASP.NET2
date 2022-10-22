using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UTNCurso.Common.Entities;
using UTNCurso.DAL.EFCore;
using UTNCurso.DAL.EFCore.Repository;

namespace UTNCurso.BLL.Bootstrappers
{
    public static class DbBootstrapper
    {
        public static IServiceCollection SetupDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlite(connectionString ?? throw new InvalidOperationException("Connection string 'TodoContext' not found.")));
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();

            return services;
        }

        public static IdentityBuilder SetupIdentity(this IdentityBuilder builder)
        {
            builder.AddEntityFrameworkStores<TodoContext>();

            return builder;
        }
    }
}