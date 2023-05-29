using CandidateApiProject.Interface;
using CandidateApiProject.Models;
using CandidateApiProject.Services;
using Microsoft.AspNetCore.Mvc;


namespace CandidateApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [ProducesResponseType(typeof(ApiResponse<Transaction>), StatusCodes.Status200OK)]
        [HttpPost, Route("createSaleTransaction")]
        public async Task<IActionResult> CreateSaleTransaction([FromBody] Transaction request)
        {
            var response = await _transactionService.CreateSaleTransaction(request);
            return Ok(response);
        }
    }
}
