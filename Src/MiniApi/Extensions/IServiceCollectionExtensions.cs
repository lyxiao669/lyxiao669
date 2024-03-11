using AdminAPI.Infrastructure.Services;
using Applet.API.Application;
using Applet.API.Infrastructure;
using MiniApi.Application;
using Domain.SeedWork;
using Infrastructure;
using Juzhen.MiniProgramAPI.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Juzhen.MiniProgramAPI
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddMyClients(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<Applet.API.Application.WechatClient>(configuration.GetSection("WechatSettings"));
            services.AddHttpClient<WechatClient>();
            return services;
        }
        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddSingleton<IPhotoService, PhotoService>();
        }

        public static void AddUsersService(this IServiceCollection services){
            services.AddTransient<IIUsersService, IUsersService>();
            // services.AddSingleton<IPhotoService, PhotoService>();
        }

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

        ///// <summary>
        ///// 注册第三方api服务
        ///// </summary>
        ///// <param name="services"></param>
        ///// <returns></returns>
        //public static IServiceCollection AddApiServices(this IServiceCollection services)
        //{
        //    services.AddHttpClient<IRandomService>();
        //    return services;
        //}

        public static IServiceCollection AddIdWorker(this IServiceCollection services)
        {
            services.AddSingleton<IIdWorker>(s =>
            {
                return new IdWorker(0);
            });
            return services;
        }
        /// <summary>
        /// 身份认证
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyAuthentication(this IServiceCollection services)
        {
            var jwtOptions = new MyJwtBearerOptions
            {
                Audience = "xiao",
                Issuer = "xiao",
                // Key = "mVsuVOI3ocFHa^3l"
                Key = "mVsuVOI3ocFHa^3lExtendedToEnsure256BitsLength"
            };
            services.AddSingleton(s =>
            {
                return new JwtSecurityTokenService(jwtOptions);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var signningKey = System.Text.Encoding.UTF8.GetBytes(jwtOptions.Key);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(signningKey),
                    ValidateLifetime = false,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateActor = true,
                    ValidateIssuer = true,
                    LifetimeValidator=null
                };
                //options.Events = new JwtBearerEvents
                //{
                //    OnAuthenticationFailed = context =>
                //    {
                //        throw new ServiceException("身份认证失败", 401);
                //    }
                //};
            });
            return services;
        }
        public static IServiceCollection ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var key = context.ModelState.Keys.First();
                    var value = context.ModelState.GetValueOrDefault(key);
                    var message = value?.Errors.FirstOrDefault()?.ErrorMessage;
                    throw new ServiceException(message, 400);
                };
            });
            return services;
        }
        public static IServiceCollection AddMyUserAccessor(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetRequiredService<IHttpContextAccessor>();
                var nameId = accessor.HttpContext.User
                    .FindFirst(a => a.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0";

                var fullName = accessor.HttpContext.User
                    .FindFirst(a => a.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

                var mobile = accessor.HttpContext.User
                    .FindFirst(a => a.Type == System.Security.Claims.ClaimTypes.MobilePhone)?.Value;

                var id = Convert.ToInt32(nameId);

                return new UserAccessor(id, fullName,mobile);
            });
            return services;
        }
        /// <summary>
        /// 解析用户详情
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMyUsersAccessor(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetRequiredService<IHttpContextAccessor>();
                var nameId = accessor.HttpContext.User
                    .FindFirst(a => a.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0";

                var userName = accessor.HttpContext.User
                    .FindFirst(a => a.Type == System.Security.Claims.ClaimTypes.Name)?.Value;

                var avatar = accessor.HttpContext.User
                    .FindFirst(a => a.Type == System.Security.Claims.ClaimTypes.MobilePhone)?.Value;

                var id = Convert.ToInt32(nameId);

                return new UsersAccessor(id, userName,avatar);
            });
            return services;
        }

        public static IServiceCollection AddRedisConnection(this IServiceCollection services)
        {
            services.AddSingleton<StackExchange.Redis.IConnectionMultiplexer>(s =>
            {
                var configuration = s.GetRequiredService<IConfiguration>();
                return StackExchange.Redis.ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            });
            return services;
        }
    }
}
