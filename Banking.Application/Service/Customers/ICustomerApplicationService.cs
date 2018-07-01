namespace Banking.Application.Service.Customers
{
    using Banking.Application.Dto.Customers;
    using Common;

    public interface ICustomerApplicationService : IBaseApplicationService<CustomerInputDto, CustomerOutputDto>
    {
        CustomerOutputDto GetByDni(string dni);

        CustomerIdentityOutputDto GetWithIdentity(int id);
    }
}
