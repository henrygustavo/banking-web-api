namespace Banking.Infrastructure.Repository.Common
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BankingContext>

    {
        public BankingContext CreateDbContext(string[] args)
        {
        
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();
            var builder = new DbContextOptionsBuilder<BankingContext>();
            // var connectionString = ("Server = (localdb)\\MSSQLLocalDB; Database = BankingDB; Integrated Security = SSPI; MultipleActiveResultSets = true");
            //builder.UseSqlServer(connectionString);
            var mySqlConnection = "server = XXXX - XXXX; port = 3306; database = XXXX - XXXX; uid = XXXX - XXXX; password = XXXX - XXXX";
            builder.UseMySql(mySqlConnection);
            return new BankingContext(builder.Options);
        }
    }
}
