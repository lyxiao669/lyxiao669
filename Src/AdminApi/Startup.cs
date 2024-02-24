using AdminApi.Application;
using Juzhen.IdentityUI;
using Infrastructure;
using Juzhen.Qiniu.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace AdminApi
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
            services.AddApiServices();
            services.AddIdWorker();
            services.AddHttpContextAccessor();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
            services.AddMediatR(GetType().Assembly);
            services.AddServices();
            //IDistributedCache
            services.AddDistributedMemoryCache();
            services.AddControllers();
            services.AddApplicationQueries();
            services.AddApplicationRepositories();
            services.AddScoped<IJwtSecurityTokenService, JwtSecurityTokenService>();
            services.AddDbContext<ApplicationDbContext>(s =>
            {
                var conStr = Configuration.GetConnectionString("MySql");
                s.UseMySql(conStr, ServerVersion.AutoDetect(conStr), o => o.MigrationsAssembly(GetType().Assembly.FullName));
                s.UseLazyLoadingProxies();
                s.UseSouExtension();
            });
            services.AddIdentityUI<DefaultIdentityUIDbContext>(s =>
            {
                var conStr = Configuration.GetConnectionString("MySql");
                s.UseMySql(conStr, ServerVersion.AutoDetect(conStr), o => o.MigrationsAssembly(GetType().Assembly.FullName));
                s.UseTablePrefix("__");
            });
            services.AddAutoMapper(c =>
            {
                c.AddProfile<AutoProfile>();
            });
            services.AddCors(p =>
            {
                p.AddPolicy("any", (o) => {
                    o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
                {
                    var options = new DefaultJwtBearerOptions();
                    c.RequireHttpsMetadata = false;
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(options.SigningKey)),
                        ValidateLifetime = false,
                        ValidIssuer = options.Issuer,
                        ValidAudience = options.Audience,
                        ValidateIssuerSigningKey = true,
                        ValidateActor = true,
                        ValidateIssuer = true
                    };
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdminApi", Version = "v1" });
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
                    $"AdminApi.xml",
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
                #endregion
            });

            services.Configure<JsonOptions>(c =>
            {
                c.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                c.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter("yyyy-MM-dd HH:mm:ss"));
                c.JsonSerializerOptions.Converters.Add(new JsonDateTimeNullableConverter("yyyy-MM-dd HH:mm:ss"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdminApi v1"));
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
