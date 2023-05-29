using CandidateApiProject.Models;

namespace CandidateApiProject.Interface
{
    public interface ITransactionService
    {
        Task<ApiResponse<Transaction>> CreateSaleTransaction(Transaction transaction);
    }
}
