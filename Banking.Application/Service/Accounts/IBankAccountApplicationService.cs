namespace Banking.Application.Service.Accounts
{
    using Banking.Application.Dto.Accounts;
    using Common;

    public interface IBankAccountApplicationService : IBaseApplicationService<BankAccountInputDto, BankAccountInputUpdateDto, BankAccountOutputDto>
    {
        BankAccountNumberOutputDto GenerateAccountNumber();

        BankAccountNumberOutputDto GetAccountNumber(int id);
    }
}
