namespace Banking.Api.Controllers.Accounts
{
    using Banking.Application.Dto.Accounts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Banking.Application.Service.Accounts;
    using Banking.Application.Dto.Common;

    [Produces("application/json")]
    [Route("api/bankaccounts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    public class BankAccountsController : Controller
    {
        private readonly IBankAccountApplicationService _bankAccountApplicationService;

        public BankAccountsController(IBankAccountApplicationService bankAccountApplicationService)
        {
            _bankAccountApplicationService = bankAccountApplicationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginationOutputDto), 200)]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            return Ok(_bankAccountApplicationService.GetAll(page, pageSize));
        }

        // GET: api/bankaccounts/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BankAccountOutputDto), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_bankAccountApplicationService.Get(id));
        }
        
        // POST: api/bankaccounts
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post([FromBody]BankAccountInputDto bankAccount)
        {
            _bankAccountApplicationService.Add(bankAccount);
            return Ok("bank account was added successfully");
        }
        
        // PUT: api/bankaccounts/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Put(int id, [FromBody]BankAccountInputUpdateDto bankAccount)
        {
            _bankAccountApplicationService.Update(id, bankAccount);
            return Ok("bank account was updated successfully");
        }

        [HttpGet("newNumber")]
        [ProducesResponseType(typeof(BankAccountNumberOutputDto), 200)]
        public IActionResult GenerateAccountNumber()
        {
            return Ok(_bankAccountApplicationService.GenerateAccountNumber());
        }
    }
}
