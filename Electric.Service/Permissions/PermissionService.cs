using AutoMapper;
using Electric.EntityFrameworkCore;
using Electric.Service.Commons;
using Electric.Entity.Permissions;
using Electric.Entity.Users;
using Electric.Entity.Roles;
using Electric.Repository;

namespace Electric.Service.Permissions
{
    public class PermissionService : BaseService, IPermissionService
    {
        private readonly IBaseRepository<ElePermission> _permissionsRepository;
        private readonly IBaseRepository<EleUser> _userRepository;

        private readonly IMapper _mapper;

        public PermissionService(IBaseRepository<ElePermission> permissionsRepository, IBaseRepository<EleUser> userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _permissionsRepository = permissionsRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public List<PermissionDto> GetAll()
        {
            var permissionDtos = new List<PermissionDto>();
            var list = _permissionsRepository.GetAll();

            foreach (var item in list)
            {
                var permissionDto = _mapper.Map<PermissionDto>(item);

                //创建者
                var creator = _userRepository.Get(permissionDto.CreatorId);
                permissionDto.Creator = _mapper.Map<UserDto>(creator);
                permissionDtos.Add(permissionDto);
            }

            return permissionDtos;
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ElePermission? Get(long id)
        {
            var item = _permissionsRepository.Get(id);
            return item;
        }

        /// <summary>
        /// 更新权限信息
        /// </summary>
        /// <param name="elePermission"></param>
        /// <returns></returns>
        public bool Update(ElePermission elePermission)
        {
            var count = _permissionsRepository.Count(x => x.Code == elePermission.Code && x.Id != elePermission.Id);
            if (count > 0)
            {
                return false;
            }

            elePermission.LastModificationTime = DateTime.Now;
            _permissionsRepository.Update(elePermission);

            return true;
        }

        /// <summary>
        /// 更新权限信息
        /// </summary>
        /// <param name="elePermission"></param>
        /// <returns></returns>
        public ElePermission Add(ElePermission elePermission)
        {
            var count = _permissionsRepository.Count(x => x.Code == elePermission.Code);
            if (count > 0)
            {
                return null;
            }

            _permissionsRepository.Add(elePermission);

            return elePermission;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id)
        {
            var entity = _permissionsRepository.Get(id);
            if (entity == null)
            {
                return false;
            }

            _permissionsRepository.Delete(entity);

            return true;
        }
    }
}
