using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Banking.Application.Service.Common
{
    public interface IBaseApplicationService<TEntity>  where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
