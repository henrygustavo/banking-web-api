namespace Banking.Api.Controllers.Identities
{
    using Banking.Application.Dto.Identities;
    using Banking.Application.Service.Identities;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IIdentityUserApplicationService _identityUserApplicationService;

        public AuthController(IIdentityUserApplicationService identityUserApplicationService)
        {
            _identityUserApplicationService = identityUserApplicationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(JwTokenOutputDto), 200)]
        public IActionResult Login([FromBody] CredentialInputDto credential)
        {
            return Ok(_identityUserApplicationService.PerformAuthentication(credential));
        }
    }
}