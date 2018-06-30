namespace Banking.Domain.Repository.Common
{
    using Accounts;
    using Customers;
    using System;
    using Identities;

    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IBankAccountRepository BankAccounts { get; }
        IIdentityUserRepository IdentityUsers { get; }
        int Complete();
    }
}
