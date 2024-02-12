using Juzhen.Infrastructure;
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
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.CompositePictureApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //七牛相关配置
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
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddMediatR(GetType().Assembly);
            //IDistributedCache
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddApplicationQueries();
            services.AddApplicationRepositories();
            services.AddDbContext<ApplicationDbContext>(s =>
            {
                var conStr = Configuration.GetConnectionString("MySql");
                s.UseMySql(conStr, ServerVersion.AutoDetect(conStr), o => o.MigrationsAssembly(GetType().Assembly.FullName));
                s.UseLazyLoadingProxies();
                s.UseSouExtension();
            });
            services.AddCors(p =>
            {
                p.AddPolicy("any", (o) => {
                    o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Juzhen.AiYanJing.CompositePictureApi", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "请输入OAuth接口返回的Token，前置Bearer。示例：Bearer {Token}",
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
                #region 注释
                var filenames = new string[]
                {
                    $"Juzhen.AiYanJing.CompositePictureApi.xml",
                    $"Juzhen.Domain.xml"
                };
                foreach (var item in filenames)
                {
                    var file = System.IO.Path.Combine(AppContext.BaseDirectory, item);
                    if (System.IO.File.Exists(file))
                    {
                        c.IncludeXmlComments(file, true);
                    }
                }
                #endregion
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Juzhen.AiYanJing.CompositePictureApi v1"));
            }

            app.UseCors("any");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<ApiExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
