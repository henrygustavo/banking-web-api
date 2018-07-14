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
        [ProducesResponseType(typeof(PaginationOutputDto), 200)]
        public IActionResult GetAll(int page = 1, int pageSize = 10, string sortBy = "lastName", string sortDirection = "asc")
        {
            return Ok(_customerApplicationService.GetAll(page, pageSize, sortBy, sortDirection));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerIdentityOutputDto),200)]
        public IActionResult Get(int id)
        {
            return Ok(_customerApplicationService.GetWithIdentity(id));
        }

        [HttpGet("dni/{dni}")]   
        [ProducesResponseType(typeof(CustomerOutputDto), 200)]
        public IActionResult Get(string dni)
        {
            return Ok(_customerApplicationService.GetByDni(dni));
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post([FromBody]CustomerInputDto customer)
        {
            _customerApplicationService.Add(customer);
            return Ok("customer was added sucessfully");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Put(int id, [FromBody]CustomerInputUpdateDto customer)
        {
            _customerApplicationService.Update(id, customer);
            return Ok("customer was updated sucessfully");
        }
    }
}