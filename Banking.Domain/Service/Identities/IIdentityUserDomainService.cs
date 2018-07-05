namespace Banking.Domain.Service.Identities
{
    using Banking.Domain.Entity.Identities;

    public interface IIdentityUserDomainService
    {
        void PerformNewUser(IdentityUser newIdentityUser,
            IdentityUser identityUserWithSameByEmail,
            IdentityUser identityUserWitSameUserName);


        string PerformAuthentication(IdentityUser identityUser,
            string loginUserName, string loginPassword,
            string jwKey, string jwIssuer);
    }
}
