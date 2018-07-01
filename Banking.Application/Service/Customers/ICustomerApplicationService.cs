namespace Banking.Application.Service.Customers
{
    using Banking.Application.Dto.Customers;
    using Common;

    public interface ICustomerApplicationService : IBaseApplicationService<CustomerInputDto, CustomerDto>
    {
        CustomerDto GetByDni(string dni);

        CustomerIdentityDto GetWithIdentity(int id);
    }
}
