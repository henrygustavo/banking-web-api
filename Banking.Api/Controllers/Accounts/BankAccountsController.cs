namespace Banking.Api.Controllers.Accounts
{
    using Banking.Application.Dto.Accounts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Banking.Application.Service.Accounts;
    using Banking.Application.Dto.Common;

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
        [ProducesResponseType(typeof(PaginationOutputDto), 200)]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            return Ok(_accountApplicationService.GetAll(page, pageSize));
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BankAccountOutputDto), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_accountApplicationService.Get(id));
        }
        
        // POST: api/Accounts
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post([FromBody]BankAccountInputDto bankAccount)
        {
            _accountApplicationService.Add(bankAccount);
            return Ok("bank account was added successfully");
        }
        
        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Put(int id, [FromBody]BankAccountInputDto bankAccount)
        {
            _accountApplicationService.Update(id, bankAccount);
            return Ok("bank account was updated successfully");
        }

        [HttpGet("new-number")]
        [ProducesResponseType(typeof(BankAccountNumberOutputDto), 200)]
        public IActionResult GenerateAccountNumber()
        {
            return Ok(_accountApplicationService.GenerateAccountNumber());
        }
    }
}
