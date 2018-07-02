namespace Banking.Application.Service.Accounts
{
    using Banking.Application.Dto.Accounts;
    using Common;

    public interface IAccountApplicationService : IBaseApplicationService<BankAccountInputDto, BankAccountOutputDto>
    {
        BankAccountNumberOutputDto GenerateAccountNumber();

        BankAccountNumberOutputDto GetAccountNumber(int id);
    }
}
