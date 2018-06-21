namespace Banking.Infrastructure.Repository.Common
{
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

        }
    }
}
