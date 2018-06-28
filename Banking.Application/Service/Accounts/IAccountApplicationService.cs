namespace Banking.Application.Service.Accounts
{
    using Banking.Application.Dto.Accounts;
    using Banking.Application.Service.Common;

    public interface IAccountApplicationService : IBaseApplicationService<BankAccountDto>
    {
    }
}
