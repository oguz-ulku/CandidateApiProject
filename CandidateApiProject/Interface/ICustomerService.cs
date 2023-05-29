using CandidateApiProject.Models;

namespace CandidateApiProject.Interface
{
    public interface ICustomerService
    {
        Task<ApiResponse<Customer>> CreateCustomer(Customer customer);
        Task<ApiResponse<Customer>> GetCustomerById(string id);
    }
}
