namespace Banking.Api.Controllers.Customers
{
    using Microsoft.AspNetCore.Mvc;
    using Banking.Application.Dto.Identities;
    using Banking.Application.Service.Identities;

    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IIdentityUserApplicationService _identityUserApplicationService;

        public AuthController(IIdentityUserApplicationService identityUserApplicationService)
        {
            _identityUserApplicationService = identityUserApplicationService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] CredentialDto credential)
        {
            return Ok(_identityUserApplicationService.PerformAuthentication(credential));
        }
    }
}