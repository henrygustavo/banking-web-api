using System.Collections.Generic;

namespace Banking.Application.Service.Common
{
    public interface IBaseApplicationService<TEntity>  where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        int Add(TEntity entity);

        int Update(int id, TEntity entity);

        int Remove(int id);
    }
}
