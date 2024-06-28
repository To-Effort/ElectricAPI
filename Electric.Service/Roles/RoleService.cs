using Electric.Service.Commons;
using AutoMapper;
using Electric.EntityFrameworkCore;
using Electric.Entity.Roles;
using Electric.Entity.Users;
using Electric.Repository;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace Electric.Service.Roles
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleService : BaseService, IRoleService
    {
        private readonly IBaseRepository<EleRole> _roleRepository;
        private readonly IBaseRepository<EleUser> _userRepository;

        private readonly IMapper _mapper;

        public RoleService(IBaseRepository<EleRole> roleRepository, IBaseRepository<EleUser> userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 搜索角色
        /// </summary>
        /// <param name="rolePageRequestDto"></param>
        /// <returns></returns>
        public RolePageResponseDto Query(RolePageRequestDto rolePageRequestDto)
        {
            var result = new RolePageResponseDto();
            var query = _roleRepository.GetQueryable();
            if (!string.IsNullOrEmpty(rolePageRequestDto.Name))
            {
                query = query.Where(x => x.Name.Equals(rolePageRequestDto.Name));
            }
            rolePageRequestDto.Page = rolePageRequestDto.Page < 1 ? 1 : rolePageRequestDto.Page;

            //获取总数、角色列表
            var count = query.Count();
            var skip = (rolePageRequestDto.Page - 1) * rolePageRequestDto.PrePage;
            var list = _roleRepository.GetPagedList(skip, rolePageRequestDto.PrePage, query);
            result.Page = rolePageRequestDto.Page;
            result.PrePage = rolePageRequestDto.PrePage;
            result.Total = count;

            //转换实体
            var roleDtos = new List<RoleDto>();
            foreach (var item in list)
            {
                var roleDto = _mapper.Map<RoleDto>(item);

                //创建者
                var creator = _userRepository.Get(roleDto.CreatorId);
                roleDto.Creator = _mapper.Map<UserDto>(creator);
                roleDtos.Add(roleDto);
            }
            result.Roles = roleDtos;


            return result;
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <param name="rolePageRequestDto"></param>
        /// <returns></returns>
        public List<RoleDto> GetAll()
        {
            var list = _roleRepository.GetAll();

            //转换实体
            var roleDtos = new List<RoleDto>();
            foreach (var item in list)
            {
                var roleDto = _mapper.Map<RoleDto>(item);

                //创建者
                var creator = _userRepository.Get(roleDto.CreatorId);
                roleDto.Creator = _mapper.Map<UserDto>(creator);
                roleDtos.Add(roleDto);
            }

            return roleDtos;
        }
    }
}
