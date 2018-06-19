namespace Banking.Domain.Repository.Accounts
{
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Repository.Common;

    public interface IBankAccountRepository : IRepository<BankAccount>
    {
    }
}
