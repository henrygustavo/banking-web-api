namespace Banking.Domain.Service.Accounts
{
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Entity.Customers;

    public interface IBankAccountDomainService
    {
        void PerformNewBankAccount(Customer customer,
            BankAccount bankAccountWithSameNumber,
            string accountNumber, bool isLocked);

        void PerformUpdateBankAccount(BankAccount bankAccount, bool isLocked);
    }
}
