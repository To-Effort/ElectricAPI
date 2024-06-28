using Electric.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Electric.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 返回查询条件表达式
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> GetQueryable()
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        /// <summary>
        /// 根据主键获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity? Get(long id)
        {

            return _dbContext.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 获取翻页数据
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public List<TEntity> GetPagedList(int skip, int take, IQueryable<TEntity> queryable)
        {
            return queryable.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
        public T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetQueryable());
        }

        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            var result = _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        /// <summary>
        /// 批量新增记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int AddRange(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(long id)
        {
            var entity = Get(id);
            if (entity == null)
            {
                return 0;
            }
            return Delete(entity);
        }

        /// <summary>
        /// 根据筛选条件，批量删除记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ExecuteDelete();
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Count(predicate);
        }

        /// <summary>
        /// 根据筛选条件，获取记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }
    }
}
