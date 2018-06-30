namespace Banking.Infrastructure.Repository.Identities
{
    using Banking.Domain.Entity.Identities;
    using Banking.Domain.Repository.Identities;
    using System.Linq;
    using Common;
    using Microsoft.EntityFrameworkCore;

    public class IdentityUserRepository : IIdentityUserRepository
    {
        protected readonly DbContext Context;

        public IdentityUserRepository(BankingContext context)
        {
            Context = context;
        }

        public IdentityUser GetByUserName(string userName)
        {
            return Context.Set<IdentityUser>().Where
                (s => s.UserName == userName).Include(p => p.Customer).FirstOrDefault();
        }

        public void Add(IdentityUser identityUser)
        {
            Context.Set<IdentityUser>().Add(identityUser);
        }
    }
}
