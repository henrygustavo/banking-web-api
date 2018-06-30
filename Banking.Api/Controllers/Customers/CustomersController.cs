namespace Banking.Api.Controllers.Customers
{
    using Banking.Application.Dto.Customers;
    using Banking.Application.Service.Customers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;

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
        public IActionResult GetAll(int page = 1, int pageSize = 10)
        {
            return Ok(_customerApplicationService.GetAll(page, pageSize));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_customerApplicationService.Get(id));
        }

        [HttpGet("dni/{dni}")]
        public IActionResult Get(string dni)
        {
            return Ok(_customerApplicationService.GetByDni(dni));
        }

        [HttpPost]
        public IActionResult Post([FromBody]CustomerDto customer)
        {
            return Ok(_customerApplicationService.Add(customer));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerDto customer)
        {
            return Ok(_customerApplicationService.Update(id, customer));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(_customerApplicationService.Remove(id));
        }
    }
}