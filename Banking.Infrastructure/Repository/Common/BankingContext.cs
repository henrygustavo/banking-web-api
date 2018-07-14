namespace Banking.Infrastructure.Repository.Common
{
    using Banking.Domain.Entity.Identities;
    using Banking.Domain.Entity.Accounts;
    using Banking.Domain.Entity.Customers;
    using Microsoft.EntityFrameworkCore;

    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options):
               base(options)
        {
         
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }

        public virtual DbSet<IdentityUser> IdentityUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                        .HasMany(c => c.BankAccounts)
                        .WithOne(e => e.Customer);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.IdentityUser)
                .WithOne(e => e.Customer);

            modelBuilder.Entity<Customer>().ToTable("customers");
            modelBuilder.Entity<BankAccount>().ToTable("bank_accounts");
            modelBuilder.Entity<IdentityUser>().ToTable("identity_users");

            
        }
    }
}
