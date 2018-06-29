namespace Banking.Application.Service.Common
{
    using System.Collections.Generic;
    using Banking.Application.Dto.Common;

    public interface IBaseApplicationService<TEntity>  where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        PaginationResultDto GetAll(int page = 1, int pageSize = 10);

        int Add(TEntity entity);

        int Update(int id, TEntity entity);

        int Remove(int id);
    }
}
