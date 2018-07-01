namespace Banking.Application.Service.Identities
{
    using Banking.Application.Dto.Identities;

    public interface IIdentityUserApplicationService
    {
        JwTokenOutputDto PerformAuthentication(CredentialInputDto credential);
    }
}
