namespace Banking.Api.Controllers.Customers
{
    using Banking.Application.Dto.Customers;
    using Banking.Application.Service.Customers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Banking.Application.Dto.Common;

    [Produces("application/json")]
    [Route("api/customers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    public class CustomersController : Controller
    {
        private readonly ICustomerApplicationService _customerApplicationService;

        public CustomersController(ICustomerApplicationService customerApplicationService)
        {
            _customerApplicationService = customerApplicationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginationResultDto), 200)]
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            return Ok(_customerApplicationService.GetAll(page, pageSize));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerIdentityDto),200)]
        public IActionResult Get(int id)
        {
            return Ok(_customerApplicationService.GetWithIdentity(id));
        }

        [HttpGet("dni/{dni}")]   
        [ProducesResponseType(typeof(CustomerDto), 200)]
        public IActionResult Get(string dni)
        {
            return Ok(_customerApplicationService.GetByDni(dni));
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult Post([FromBody]CustomerInputDto customer)
        {
            return Ok(_customerApplicationService.Add(customer));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult Put(int id, [FromBody]CustomerInputDto customer)
        {
            return Ok(_customerApplicationService.Update(id, customer));
        }
    }
}