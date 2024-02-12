
using Juzhen.Domain.SeedWork;
using Juzhen.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Juzhen.AiYanJing.CompositePictureApi
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
    }
}
