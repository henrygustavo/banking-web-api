namespace Banking.Application.Service.Customers
{
    using Banking.Application.Dto.Customers;
    using Banking.Application.Dto.Transactions;
    using Common;
    using System.Collections.Generic;

    public interface ICustomerApplicationService : IBaseApplicationService<CustomerInputDto, CustomerInputUpdateDto, CustomerOutputDto>
    {
        CustomerOutputDto GetByDni(string dni);

        CustomerIdentityOutputDto GetWithIdentity(int id);

        List<CustomerBankTransferOtputDto> GetBankTransfersById(int id);
    }
}
