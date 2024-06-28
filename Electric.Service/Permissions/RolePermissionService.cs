using Electric.EntityFrameworkCore;
using Electric.Entity.Accounts;
using Electric.Entity.Permissions;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Linq;
using Electric.Entity.Commons;
using AutoMapper;
using Electric.Service.Commons;
using Electric.Entity.Roles;
using Electric.Repository;

namespace Electric.Service.Permissions
{
    /// <summary>
    /// 角色权限关系
    /// </summary>
    public class RolePermissionService : BaseService, IRolePermissionService
    {
        private readonly IBaseRepository<ElePermission> _permissionsRepository;
        private readonly IBaseRepository<EleRolePermission> _rolePermissionRepository;
        private readonly IBaseRepository<EleRole> _roleRepository;

        private readonly IMapper _mapper;

        public RolePermissionService(IBaseRepository<ElePermission> permissionsRepository, IBaseRepository<EleRolePermission> rolePermissionRepository, IBaseRepository<EleRole> roleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _permissionsRepository = permissionsRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<AccountPermissionsDto> GetRolePermissions(string roleName)
        {
            //根据角色获取权限列表
            var permissions = (from p in _permissionsRepository.GetQueryable()
                               join rp in _rolePermissionRepository.GetQueryable() on p.Id equals rp.PermissionId
                               join r in _roleRepository.GetQueryable() on rp.RoleId equals r.Id
                               where r.NormalizedName == roleName && p.Status == PermissionStatus.Normal
                               select p
                     ).ToList();

            //对象映射转化
            var permissionsDtos = _mapper.Map<List<AccountPermissionsDto>>(permissions);

            return permissionsDtos;
        }

        /// <summary>
        /// 获取角色权限列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<PermissionDto> GetRolePermissions(long roleId)
        {
            var permissions = new List<PermissionDto>();
            var list = _rolePermissionRepository.GetList(x => x.RoleId == roleId);
            var mapper = MappingProfile.CreateMapper();
            foreach (var item in list)
            {
                var elePermission = _permissionsRepository.Get(item.PermissionId);

                var permissionDto = mapper.Map<PermissionDto>(elePermission);
                permissions.Add(permissionDto);
            }

            return permissions;
        }

        /// <summary>
        /// 保存角色的权限列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionIds"></param>
        /// <param name="CreatorId"></param>
        /// <returns></returns>
        public bool SavePermissions(long roleId, List<long> permissionIds, long creatorId)
        {
            _rolePermissionRepository.Delete(x=>x.RoleId == roleId);
            var list = new List<EleRolePermission>();
            foreach (var item in permissionIds)
            {
                var eleRolePermission = new EleRolePermission();
                eleRolePermission.RoleId = roleId;
                eleRolePermission.PermissionId = item;
                eleRolePermission.CreationTime = DateTime.Now;
                eleRolePermission.CreatorId = creatorId;
                list.Add(eleRolePermission);
            }

            return _rolePermissionRepository.AddRange(list) > 0;
        }
    }
}
