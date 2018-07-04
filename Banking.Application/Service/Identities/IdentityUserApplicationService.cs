namespace Banking.Application.Service.Identities
{
    using Banking.Application.Dto.Identities;
    using Banking.Domain.Repository.Common;
    using System;
    using Microsoft.Extensions.Configuration;
    using Notification;
    using Banking.Domain.Service.Identities;

    public class IdentityUserApplicationService: IIdentityUserApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IIdentityUserDomainService _identityUserDomainService;
        public IdentityUserApplicationService(IUnitOfWork unitOfWork, IConfiguration config,
            IIdentityUserDomainService identityUserDomainService)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _identityUserDomainService = identityUserDomainService;
        }

        public JwTokenOutputDto PerformAuthentication(CredentialInputDto credential)
        {
            var identityUser = _unitOfWork.IdentityUsers.GetByUserName(credential.UserName);

            Notification notification = Validation(credential);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }
            
            string accessToken = _identityUserDomainService.PerformAuthentication(identityUser, credential.UserName,
                                                            credential.Password, _config["Jwt:Key"], _config["Jwt:Issuer"]);

            return new JwTokenOutputDto { access_token = accessToken };
        }

        private Notification Validation(CredentialInputDto credential)
        {
            Notification notification = new Notification();

            if (credential != null) return notification;
            notification.AddError("Invalid JSON data in request body.");
            return notification;
        }
    }
}
