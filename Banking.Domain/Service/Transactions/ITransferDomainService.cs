namespace Banking.Domain.Service.Transactions
{
    using Entity.Accounts;

    public interface ITransferDomainService
    {
       void PerformTransfer(BankAccount originAccount, BankAccount destinationAccount, decimal amount);
    }
}
