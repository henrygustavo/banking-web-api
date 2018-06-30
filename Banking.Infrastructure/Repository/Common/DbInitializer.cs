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
            if (_context.Customers.Any(p => p.Dni == "00000001")) return;

            var identityUser = new IdentityUser
            {
                Email = "admin@test",
                UserName = "admin",
                Password = "123456",
                Role = "admin"
            };

            _context.IdentityUsers.Add(identityUser);

            var newCustomer = new Customer
            {
                Id = 1,
                FirstName = "Administrator",
                LastName = "Admin",
                Dni = "00000001",
                IdentityUserId = identityUser.Id
            };

            _context.Customers.Add(newCustomer);
        }

        public void SeedMembersOne()
        {
            if (_context.Customers.Any(p => p.Dni == "44444568")) return;

            var identityUser = new IdentityUser
            {
                Email = "henrygustavof@gmail.com",
                UserName = "henrygustavo",
                Password = "123456",
                Role = "member"
            };

            _context.IdentityUsers.Add(identityUser);

            var newCustomer = new Customer
            {
                Id = 2,
                FirstName = "Henry",
                LastName = "Fuentes",
                Dni = "44444568",
                IdentityUserId = identityUser.Id
            };

            _context.Customers.Add(newCustomer);

            _context.BankAccounts.Add(new BankAccount
            {
                Customer = newCustomer,
                Balance = 0,
                IsLocked = false,
                Number = "1234567890"
            });
        }

        public void SeedMembersTwo()
        {
            if (_context.Customers.Any(p => p.Dni == "44444569")) return;

            var identityUser = new IdentityUser
            {
                Email = "juanPerez@gmail.com",
                UserName = "juanperez",
                Password = "123456",
                Role = "member"
            };

            _context.IdentityUsers.Add(identityUser);

            var newCustomer = new Customer
            {
                Id = 3,
                FirstName = "Juan",
                LastName = "Perez",
                Dni = "44444569",
                IdentityUserId = identityUser.Id
            };

            _context.Customers.Add(newCustomer);

            _context.BankAccounts.Add(new BankAccount
            {
                Customer = newCustomer,
                Balance = 0,
                IsLocked = false,
                Number = "1234567899"
            });
        }
    }
}
