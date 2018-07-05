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
            SeedAdmin();
            SeedMembersOne();
            SeedMembersTwo();
            await _context.SaveChangesAsync();
        }

        public void SeedAdmin()
        {
            if (_context.IdentityUsers.Any(p => p.UserName == "admin")) return;

            var identityUser = new IdentityUser("admin", "admin@test", "admin", "admin", true);

            _context.IdentityUsers.Add(identityUser);
        }

        public void SeedMembersOne()
        {
            if (_context.Customers.Any(p => p.Dni == "44444568")) return;

            var identityUser = new IdentityUser
            (
              "henrygustavo",
              "henrygustavof@gmail.com",
              "123456",
              "member",
              true
            );

            _context.IdentityUsers.Add(identityUser);

            var newCustomer = new Customer
            {
                Id = 2,
                FirstName = "Henry",
                LastName = "Fuentes",
                Dni = "44444568",
                IdentityUserId = identityUser.Id,
                Active = true
            };

            _context.Customers.Add(newCustomer);

            _context.BankAccounts.Add(new BankAccount
            {
                Customer = newCustomer,
                Balance = 1000,
                IsLocked = false,
                Number = "100000000000000000"
            });
        }

        public void SeedMembersTwo()
        {
            if (_context.Customers.Any(p => p.Dni == "44444569")) return;

            var identityUser = new IdentityUser
            (
              "juanperez",
              "juanPerez@gmail.com",
              "123456",
               "member",
               true
            );

            _context.IdentityUsers.Add(identityUser);

            var newCustomer = new Customer
            {
                Id = 3,
                FirstName = "Juan",
                LastName = "Perez",
                Dni = "44444569",
                IdentityUserId = identityUser.Id,
                Active = true
            };

            _context.Customers.Add(newCustomer);

            _context.BankAccounts.Add(new BankAccount
            {
                Customer = newCustomer,
                Balance = 1000,
                IsLocked = false,
                Number = "100000000000000001"
            });
        }
    }
}
