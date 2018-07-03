namespace Banking.Domain.Service.Identities
{
    using Application.Notification;
    using Banking.Domain.Entity.Identities;

    public interface IIdentityUserDomainService
    {
        IdentityUser PerformNewUser(string userName, string email, string password,
            bool active, IdentityUser searchedIdentityUserByEmail,
            IdentityUser searchedIdentityUserByUserName);
    }
}
