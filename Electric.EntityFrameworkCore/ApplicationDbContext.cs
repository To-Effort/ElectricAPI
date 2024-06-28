using Electric.Entity;
using Electric.Entity.AuditLogs;
using Electric.Entity.Permissions;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Electric.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<EleUser, EleRole, long, EleUserClaim, EleUserRole,
        EleUserLogin, EleRoleClaim, EleUserToken>
    {
        public DbSet<ElePermission> ElePermission { get; set; }
        public DbSet<EleRolePermission> EleRolePermission { get; set; }
        public DbSet<EleAuditLog> EleAuditLog { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 模型生成器
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 配置实体类型映射到的表名
            modelBuilder.Entity<EleUser>().ToTable("EleUser");
            modelBuilder.Entity<EleUserLogin>().ToTable("EleUserLogin");
            modelBuilder.Entity<EleUserClaim>().ToTable("EleUserClaim");
            modelBuilder.Entity<EleUserToken>().ToTable("EleUserToken");

            modelBuilder.Entity<EleRole>().ToTable("EleRole");
            modelBuilder.Entity<EleRoleClaim>().ToTable("EleRoleClaim");

            modelBuilder.Entity<EleUserRole>().ToTable("EleUserRole");

            //设定字段长度
            modelBuilder.Entity<EleRole>(entity => {
                entity.Property(r => r.Name).HasMaxLength(100);
                entity.Property(r => r.NormalizedName).HasMaxLength(100);
            });
            modelBuilder.Entity<EleUser>(entity => {
                entity.Property(r => r.UserName).HasMaxLength(100);
                entity.Property(r => r.NormalizedUserName).HasMaxLength(100);
            });

            //1. 指定外键关系
            modelBuilder.Entity<EleRole>(entity =>
            {
                //指定角色与角色权限表，一对多
                entity.HasMany<EleRolePermission>().WithOne().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ElePermission>(entity =>
            {
                //指定权限与角色权限表，一对多
                entity.HasMany<EleRolePermission>().WithOne().HasForeignKey(x => x.PermissionId).OnDelete(DeleteBehavior.Cascade);
            });

            var adminRoleId = 1;
            // 2. 添加角色
            modelBuilder.Entity<EleRole>().HasData(
                new EleRole
                {
                    Id = adminRoleId,
                    CreationTime = DateTime.Now,
                    Name = "管理员",
                    NormalizedName = "管理员".ToUpper(),
                    Status = RoleStatus.Normal
                }
            );

            // 3. 添加用户
            var adminUserId = 1;
            EleUser adminUser = new EleUser
            {
                Id = adminUserId,
                CreationTime = DateTime.Now,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "admin@eletric.com",
                NormalizedEmail = "admin@eletric.com".ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = false,
                SecurityStamp = "abc",
                FullName = "管理员",
                Status = UserStatus.Normal
            };

            PasswordHasher<EleUser> ph = new PasswordHasher<EleUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Abc123@");
            modelBuilder.Entity<EleUser>().HasData(adminUser);

            // 4. 给用户加入管理员权限
            modelBuilder.Entity<EleUserRole>()
                .HasData(new EleUserRole()
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });

            //5. 初始化权限
            var permissionList = new List<ElePermission>
            {
                #region 菜单权限
                new ElePermission()
                {
                    Id = 1,
                    Name = "系统管理",
                    Code = "system",
                    Url = "system",
                    Component = "",
                    Icon = "el-icon-s-tools",
                    PermissionType = PermissionType.Menu,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 0,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                    Id = 2,
                    Name = "用户管理",
                    Code = "system.user",
                    Url = "system.user",
                    Component = "views/documentation/index",
                    Icon = "el-icon-user-solid",
                    PermissionType = PermissionType.Menu,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 1,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 3,
                    Name = "角色管理",
                    Code = "system.role",
                    Url = "role",
                    Component = "views/documentation/index",
                    Icon = "peoples",
                    PermissionType = PermissionType.Menu,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 1,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 4,
                    Name = "菜单管理",
                    Code = "system.permission",
                    Url = "permission",
                    Component = "views/documentation/index",
                    Icon = "list",
                    PermissionType = PermissionType.Menu,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 1,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 5,
                    Name = "角色权限",
                    Code = "system.rolepermission",
                    Url = "rolepermission",
                    Component = "views/documentation/index",
                    Icon = "example",
                    PermissionType = PermissionType.Menu,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 1,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                    Id = 16,
                    Name = "日志管理",
                    Code = "log.auditlog",
                    Url = "log",
                    Component = "",
                    Icon = "bug",
                    PermissionType = PermissionType.Menu,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 0,
                    Status = PermissionStatus.Normal
                },
                #endregion

                #region 按钮、元素权限
                new ElePermission()
                {
                   Id = 6,
                    Name = "添加",
                    Code = "system.user.add",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 2,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                    Id = 7,
                    Name = "编辑",
                    Code = "system.user.edit",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 2,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                    Id = 8,
                    Name = "删除",
                    Code = "system.user.delete",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 2,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 9,
                    Name = "添加",
                    Code = "system.role.add",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 3,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 10,
                    Name = "编辑",
                    Code = "system.role.edit",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 3,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 11,
                    Name = "删除",
                    Code = "system.role.delete",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 3,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 12,
                    Name = "添加",
                    Code = "system.permission.add",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 4,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                    Id = 13,
                    Name = "编辑",
                    Code = "system.permission.edit",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 4,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                    Id = 14,
                    Name = "删除",
                    Code = "system.permission.delete",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 4,
                    Status = PermissionStatus.Normal
                },
                new ElePermission()
                {
                   Id = 15,
                    Name = "更新",
                    Code = "system.rolepermission.update",
                    Url = "",
                    Component = "",
                    Icon = "",
                    PermissionType = PermissionType.Element,
                    ApiMethod = "",
                    Sort = 0,
                    ParentId = 5,
                    Status = PermissionStatus.Normal
                }
                #endregion
            };
            modelBuilder.Entity<ElePermission>().HasData(permissionList);

            // 6. 给角色分配权限
            var rolePermissionList = new List<EleRolePermission>();
            foreach (var permission in permissionList)
            {
                rolePermissionList.Add(new EleRolePermission()
                {
                    Id = permission.Id,
                    RoleId = adminRoleId,
                    PermissionId = permission.Id
                });
            }
            modelBuilder.Entity<EleRolePermission>()
                .HasData(rolePermissionList);
        }
    }
}