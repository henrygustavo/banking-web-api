namespace Banking.Infrastructure.Repository.Common
{
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Entity.Customers;
    using System.Threading.Tasks;

    public class DbInitializer
    {
        private readonly BankingContext _context;

        public DbInitializer(BankingContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task Seed()
        {
            var newCustomer = new Customer {FirstName = "Henry", LastName = "Fuentes"};

            _context.Customers.Add(newCustomer);

            _context.BankAccounts.Add(new BankAccount{ Customer = newCustomer, Balance = 0,IsLocked = false , Number = "1234567890"});

           await _context.SaveChangesAsync();
        }
    }
}
