﻿
namespace Electric.Service.UOW
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
    }
}
