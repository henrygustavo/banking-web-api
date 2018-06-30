namespace Banking.Domain.Repository.Identities
{
    using Banking.Domain.Entity.Identities;
    public interface IIdentityUserRepository
    {
        IdentityUser Get(int id);

        IdentityUser GetByUserName(string userName);
        IdentityUser GetByEmail(string email);

        void Add(IdentityUser identityUser);
    }
}
