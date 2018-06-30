namespace Banking.Api.Controllers.Customers
{
    using Microsoft.AspNetCore.Mvc;
    using Banking.Application.Dto.Identities;
    using Banking.Application.Service.Identities;

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
        public IActionResult Login([FromBody] CredentialDto credential)
        {
            string jwToken = _identityUserApplicationService.PerformAuthentication(credential);
            return Ok(new { access_token = jwToken });
        }
    }
}