namespace Banking.Domain.Repository.Common
{
    using Banking.Domain.Repository.Accounts;
    using Banking.Domain.Repository.Customers;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IBankAccountRepository BankAccounts { get; }
        int Complete();
    }
}
