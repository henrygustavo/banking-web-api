namespace Banking.Application.Service.Common
{
    using Banking.Application.Dto.Common;

    public interface IBaseApplicationService<TEntityInput ,TEntityInputUpdate, TEntityOutPut>
    {
        TEntityOutPut Get(int id);

        PaginationOutputDto GetAll(int page, int pageSize, string sortBy, string sortDirection);

        int Add(TEntityInput entity);

        int Update(int id, TEntityInputUpdate entity);

    }
}
