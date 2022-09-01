using EMS.App.Common.Interface;
using EMS.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EMS.App.Common.Config;
using EMS.Common.Interface;
using EMS.Infrastructure.Identity;
using EMS.Infrastructure.File;

namespace EMS.Infrastructure.Persistence
{
    /// <summary>
    /// 依赖注入相关功能
    /// </summary>
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // 注入数据库上下文
            services.AddDbContext<EMSDbContext>(options =>
                options.UseNpgsql(
                    // 获取链接字符串
                    config.GetConnectionString("PostgreSQL"),
                    // 应用数据库的相关设置在Migrations中
                    builder => builder.MigrationsAssembly(typeof(EMSDbContext).Assembly.FullName)
                )
            );

            // 注入 Repository 的实现
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
            // 注入验证服务
            services.AddTransient(typeof(IIdentity), typeof(IdentityService));
            services.AddScoped(typeof(IAppFileUpload), typeof(AppFileUpload));
            // 注入验证器
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;  
                    options.SaveToken = true; 

                    var tokenParameter = config.GetSection("JwtSetting").Get<JwtSetting>();
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenParameter.Issuer,
                        ValidAudience = tokenParameter.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameter.Secret))
                    };
                });

            return services;
        }
    }
}