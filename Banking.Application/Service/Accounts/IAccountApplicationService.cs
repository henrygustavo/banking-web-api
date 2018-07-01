namespace Banking.Application.Service.Accounts
{
    using Banking.Application.Dto.Accounts;
    using Common;

    public interface IAccountApplicationService : IBaseApplicationService<BankAccountInputDto, BankAccountDto>
    {
        NewBankAccountDto GenerateAccountNumber();
    }
}
