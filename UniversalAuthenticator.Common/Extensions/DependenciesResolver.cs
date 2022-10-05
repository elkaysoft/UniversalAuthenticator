using Microsoft.Extensions.DependencyInjection;
using UniversalAuthenticator.Domain.Data;
using System.Reflection;

using FluentValidation;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Services;

namespace UniversalAuthenticator.Common.Extensions
{
    public class ApplicationDependency
    {
        public static void AddApplicationDI(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
        }
    }

    public class EntityDependency
    {
        public static void AddPersistenceDI(IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
        }
    }
}
