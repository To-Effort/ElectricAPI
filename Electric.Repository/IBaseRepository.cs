using System.Linq.Expressions;


namespace Electric.Repository
{
    public interface  IBaseRepository
    {

    }

    public interface IBaseRepository<TEntity>: IBaseRepository where TEntity : class, new()
    {
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 批量新增记录
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int AddRange(List<TEntity> entities);

        /// <summary>
        /// 返回查询条件表达式
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetQueryable();

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(TEntity entity);

        /// <summary>
        /// 根据筛选条件，批量删除记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据主键获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity? Get(long id);

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        List<TEntity> GetAll();

        /// <summary>
        /// 根据筛选条件，获取记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryMethod"></param>
        /// <returns></returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        /// <summary>
        /// 获取翻页数据
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="queryable"></param>
        /// <returns></returns>
        List<TEntity> GetPagedList(int skip, int take, IQueryable<TEntity> queryable);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(long id);
    }
}
