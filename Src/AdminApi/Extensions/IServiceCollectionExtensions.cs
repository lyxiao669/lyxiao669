
using AdminApi.Application;
using Juzhen.Domain.SeedWork;
using Juzhen.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AdminApi
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 注册所有仓储
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
        {
            var queryTypes = typeof(ApplicationDbContext).Assembly
                .GetTypes().Where(a => typeof(IRepository).IsAssignableFrom(a))
                .Where(a => a.IsClass && !a.IsAbstract);
            foreach (var item in queryTypes)
            {
                var serviceType = item.GetInterfaces()
                    .Where(a => typeof(IRepository).IsAssignableFrom(a) && !a.IsGenericType)
                    .First();
                services.AddTransient(serviceType, item);
            }
            return services;
        }

        /// <summary>
        /// 注册所有查询
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationQueries(this IServiceCollection services)
        {
            var queryTypes = typeof(IServiceCollectionExtensions).Assembly
               .GetTypes().Where(a => typeof(BaseQueries).IsAssignableFrom(a))
               .Where(a => a.IsClass && !a.IsAbstract);
            foreach (var item in queryTypes)
            {
                services.AddTransient(item, item);
            }
            return services;
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IRandomService, RandomService>();
        }

        /// <summary>
        /// 注册第三方api服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddHttpClient<GaodeApiService>();
            return services;
        }

        public static IServiceCollection AddIdWorker(this IServiceCollection services)
        {
            services.AddSingleton<IIdWorker>(s =>
            {
                return new IdWorker(0);
            });
            return services;
        }
    }
}
