using EMS.App.Common.Interface;
using EMS.Domain.Entity;
using EMS.Infrastructure.Persistence;
using EnumsNET;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.Infrastructure
{
    public static class ApplicationStartup
    {
        public static async Task MigrateDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<EMSDbContext>();

                // 自动同步迁移文件到数据库服务器，前提是必须先（手动）生成迁移文件
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
                // 初始化项目运行需要的最基本的配置
                var appUser = services.GetRequiredService<IRepository<UserInfo>>();
                if (!appUser.Table.Any())
                {
                    var appRole = services.GetRequiredService<IRepository<RoleInfo>>();
                    var appUserGroup = services.GetRequiredService<IRepository<UserGroupInfo>>();
                    var appUserGroupUser = services.GetRequiredService<IRepository<UserGroupUserInfo>>();

                    var roleList = new List<RoleInfo>();
                    // 创建角色
                    foreach (var roleEnum in Enums.GetMembers<RoleType>())
                    {
                        roleList.Add(new RoleInfo { RoleName = (RoleType)(roleEnum.Value) });
                    }
                    var userAdmin = await appUser.AddAsync(new UserInfo { Account = "admin", Password = "113", Remarks = "Has all permission" });
                    var role = await appRole.AddAsync(roleList);
                    var groupInfoList = new List<UserGroupInfo>();
                    foreach (var roleInfo in role)
                    {
                        groupInfoList.Add(new UserGroupInfo
                        {
                            UserGroupName = ((RoleType)(roleInfo.RoleName)).ToString().ToLower(),
                            RoleId = roleInfo.Id,
                            limit = 0 // 不做限制
                        });
                    }

                    var userGroup = await appUserGroup.AddAsync(groupInfoList);
                    var userGroupUserInfo = new List<UserGroupUserInfo>();
                    foreach (var group in userGroup)
                    {
                        userGroupUserInfo.Add(new UserGroupUserInfo
                        {
                            UserGroupId = group.Id,
                            UserId = userAdmin.Id
                        });
                    }
                    await appUserGroupUser.AddAsync(userGroupUserInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred migrating the DB: {ex}");
            }
        }

    }
}