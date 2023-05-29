using CandidateApiProject.Interface;
using CandidateApiProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CandidateApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [ProducesResponseType(typeof(ApiResponse<Customer>), StatusCodes.Status200OK)]
        [HttpPost, Route("createCustomer")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer request)
        {
            var response = await _customerService.CreateCustomer(request);
            return Ok(response);
        }
    }
}
