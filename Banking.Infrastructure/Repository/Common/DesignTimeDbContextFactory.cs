﻿namespace Banking.Infrastructure.Repository.Common
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
            var mySqlConnection = "server=localhost;port=3306;database=bankingdb;uid=root;password=admin";
            builder.UseMySql(mySqlConnection);
            return new BankingContext(builder.Options);
        }
    }
}
