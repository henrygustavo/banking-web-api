namespace Banking.Infrastructure.Repository.Common
{
    using Banking.Domain.Repository.Accounts;
    using Banking.Domain.Repository.Common;
    using Banking.Domain.Repository.Customers;
    using Accounts;
    using Customers;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly BankingContext _context;

        public UnitOfWork(BankingContext context)
        {
            _context = context;
            Customers = new CustomerRepository(_context);
            BankAccounts = new BankAccountRepository(_context);
        }

        public ICustomerRepository Customers { get; }
        public IBankAccountRepository BankAccounts { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
