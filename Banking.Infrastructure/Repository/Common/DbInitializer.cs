namespace Banking.Infrastructure.Repository.Common
{
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Entity.Customers;
    using System.Threading.Tasks;
    using System.Linq;
    using Banking.Domain.Entity.Identities;

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
            if (!_context.Customers.Any(p => p.Dni == "00000001"))
            {
                var identityUser = new IdentityUser { Email = "admin@test", UserName = "admin",
                                                      Password = "123456" , Role = "admin"};

                _context.IdentityUsers.Add(identityUser);

                var newCustomer = new Customer { Id = 1, FirstName = "Administrator",
                                                LastName = "Admin", Dni = "00000001" , IdentityUserId = identityUser.Id};

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
