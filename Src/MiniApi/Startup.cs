using MiniApi.Application;
using Infrastructure;
using Juzhen.MiniProgramAPI;
using Juzhen.MiniProgramAPI.Infrastructure.AspNetCore;
using Juzhen.MiniProgramAPI.Infrastructure.Utils;
using Juzhen.Qiniu.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applet.API.Infrastructure;

namespace MiniApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //��ţ�������
            string qiniu_accesskey = Configuration.GetValue<string>("QiniuStrings:accesskey");
            string qiniu_secretkey = Configuration.GetValue<string>("QiniuStrings:secretkey");
            string qiniu_domain = Configuration.GetValue<string>("QiniuStrings:domain");
            string qiniu_scope = Configuration.GetValue<string>("QiniuStrings:scope");
            
            QiniuUtil.AddConfigSource(qiniu_accesskey, qiniu_secretkey, qiniu_domain, qiniu_scope);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<Wechat.Sdk.WechatOptions>(Configuration.GetSection("WechatSettings"));
            // services.AddUsersService();
            
            services.AddScoped<UsersAccessor>();
            services.AddIdentityService();
            services.AddIdWorker();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddMediatR(GetType().Assembly);
            services.AddAutoMapper(c =>
            {
                c.AddProfile<AllProfile>();
            });
            services.AddDbContext<ApplicationDbContext>(s =>
            {
                var conStr = Configuration.GetConnectionString("MySql");
                s.UseMySql(conStr, ServerVersion.AutoDetect(conStr), o => o.MigrationsAssembly(GetType().Assembly.FullName));
            });
            services.AddRedisConnection();
            services.AddHttpContextAccessor();
            services.AddMyAuthentication();
            services.AddMyClients(Configuration);
            services.AddMyUserAccessor();
            services.AddApplicationQueries();
            services.AddApplicationRepositories();
            services.AddHttpContextAccessor();
            services.ConfigureApiBehaviorOptions();

            services.AddControllers()
                .AddJsonOptions(json =>
                {
                    json.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                    json.JsonSerializerOptions.Converters.Add(new DateTimeNullableJsonConverter());
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiniApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "������OAuth�ӿڷ��ص�Token��ǰ��Bearer��ʾ����Bearer {Token}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, Array.Empty<string>() }
                });
                var filenames = new string[]
                {
                    $"MiniApi.xml",
                    $"Domain.xml"
                };
                foreach (var item in filenames)
                {
                    var file = System.IO.Path.Combine(AppContext.BaseDirectory, item);
                    if (System.IO.File.Exists(file))
                    {
                        c.IncludeXmlComments(file, true);
                    }
                }
            });
            services.AddCors(o =>
            {
                o.AddPolicy("any", p =>
                {
                    p.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniApi v1"));
            }

            //app.UseMiddleware<ApiExceptionHandlerMiddleware>();
            app.UseRouting();

            app.UseCors("any");

            app.UseMiddleware<ApiExceptionHandlerMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
