namespace Banking.Application.Service.Identities
{
    using Banking.Application.Dto.Identities;

    public interface IIdentityUserApplicationService
    {
        string PerformAuthentication(CredentialDto credential);
    }
}
