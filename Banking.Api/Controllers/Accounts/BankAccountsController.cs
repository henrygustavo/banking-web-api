namespace Banking.Api.Controllers.Accounts
{
    using Banking.Application.Dto.Accounts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Banking.Application.Service.Accounts;

    [Produces("application/json")]
    [Route("api/bank-accounts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    public class BankAccountsController : Controller
    {
        private readonly IAccountApplicationService _accountApplicationService;

        public BankAccountsController(IAccountApplicationService accountApplicationService)
        {
            _accountApplicationService = accountApplicationService;
        }

        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            return Ok(_accountApplicationService.GetAll(page, pageSize));
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_accountApplicationService.Get(id));
        }
        
        // POST: api/Accounts
        [HttpPost]
        public IActionResult Post([FromBody]BankAccountInputDto bankAccount)
        {
            return Ok(_accountApplicationService.Add(bankAccount));
        }
        
        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]BankAccountInputDto bankAccount)
        {
            return Ok(_accountApplicationService.Update(id, bankAccount));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("new-account")]
        public IActionResult GenerateAccountNumber()
        {
            return Ok(new { accountNumber =_accountApplicationService.GenerateAccountNumber()});
        }
    }
}
