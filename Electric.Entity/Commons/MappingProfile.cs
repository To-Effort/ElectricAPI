using Electric.Entity.Permissions;
using AutoMapper;
using Electric.Entity.Accounts;
using Electric.Entity.Users;
using Electric.Entity.Roles;

namespace Electric.Entity.Commons
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ElePermission, AccountPermissionsDto>();
            CreateMap<ElePermission, PermissionDto>();
            CreateMap<PermissionCreateDto, ElePermission>();
            CreateMap<PermissionUpdateDto, ElePermission>();

            CreateMap<EleUser, UserDto>();
            CreateMap<EleUser, UserAllDto>();
            CreateMap<UserUpdateDto, EleUser>();
            CreateMap<UserCreateDto, EleUser>();

            CreateMap<EleRole, RoleDto>();
            CreateMap<RoleCreateDto, EleRole>();
            CreateMap<RoleUpdateDto, EleRole>();
        }

        /// <summary>
        /// 创建映射对象
        /// </summary>
        /// <returns></returns>
        public static IMapper CreateMapper()
        {
            //创建配置对象
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            //创建映射对象
            var mapper = configuration.CreateMapper();

            return mapper;
        }

    }
}
