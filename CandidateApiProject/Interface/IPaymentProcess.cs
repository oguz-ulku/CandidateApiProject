using CandidateApiProject.Models;

namespace CandidateApiProject.Interface
{
    public interface IPaymentProcess
    {
        Task<ApiResponse<VposResponse>> Sale(Transaction transaction);
        Task<ApiResponse<VposResponse>> Void(Transaction transaction);
    }
}
