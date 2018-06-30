namespace Banking.Application.Service.Identities
{
    using Banking.Application.Dto.Identities;
    using Banking.Domain.Repository.Common;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Extensions.Configuration;
    using System.Security.Claims;

    public class IdentityUserApplicationService: IIdentityUserApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public IdentityUserApplicationService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public string PerformAuthentication(CredentialDto credential)
        {
            var identityUser = _unitOfWork.IdentityUsers.GetByUserName(credential.UserName);

            if (identityUser.HasValidCredentials(credential.UserName, credential.Password))
            {
                return BuildToken(identityUser.Customer.Id, identityUser.UserName, identityUser.Role);
            }

            return string.Empty;
        }

        private string BuildToken(int customerId, string userName, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("customerId", customerId.ToString()),
                new Claim("userName", userName),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
