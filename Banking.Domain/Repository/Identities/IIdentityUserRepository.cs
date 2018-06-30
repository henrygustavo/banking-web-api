namespace Banking.Domain.Repository.Identities
{
    using Banking.Domain.Entity.Identities;
    public interface IIdentityUserRepository
    {
        IdentityUser GetByUserName(string userName);

        void Add(IdentityUser identityUser);
    }
}
