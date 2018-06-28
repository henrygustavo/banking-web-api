namespace Banking.Api.Controllers.Customers
{
    using Banking.Application.Dto.Customers;
    using Banking.Application.Service.Customers;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Produces("application/json")]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerApplicationService _customerApplicationService;

        public CustomersController(ICustomerApplicationService customerApplicationService)
        {
            _customerApplicationService = customerApplicationService;
        }

        [HttpGet]
        public IEnumerable<CustomerDto> GetAll()
        {

            return _customerApplicationService.GetAll();
        }
    }
}