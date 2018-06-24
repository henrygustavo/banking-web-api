namespace Banking.Infrastructure.Repository.Common
{
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Entity.Customers;
    using System.Threading.Tasks;
    using System.Linq;

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
            if (!_context.Customers.Any(p => p.Dni == "44444567"))
            {
                var newCustomer = new Customer { Id = 1, FirstName = "Henry", LastName = "Fuentes", Dni = "44444567"};

                _context.Customers.Add(newCustomer);

                _context.BankAccounts.Add(new BankAccount
                {
                    Customer = newCustomer,
                    Balance = 0,
                    IsLocked = false,
                    Number = "1234567890"
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
