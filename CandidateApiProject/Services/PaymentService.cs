using CandidateApiProject.Exceptions;
using CandidateApiProject.Interface;
using CandidateApiProject.Mapper;
using CandidateApiProject.Models;
using Microsoft.Extensions.Options;

namespace CandidateApiProject.Services
{
    public class PaymentService: IPaymentProcess
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationSettings _config;
        private readonly IHttpService _httpService;
        public PaymentService(ICustomerService customerService, IOptions<ApplicationSettings> config, IHttpService httpService)
        {
            _customerService = customerService;
            _config = config.Value;
            _httpService = httpService;
        }
        public async Task<ApiResponse<VposResponse>> Sale(Transaction transaction)
        {
            var responseCode = GenericEnums.SuccessCode;
            var responseMessage = GenericEnums.SuccessMessage;
            var fail = false;
            var statusCode = 200;
            var paymentResponse = new VposResponse();

            try
            {
                var customer = await _customerService.GetCustomerById(transaction.CustomerId.ToString());

                if (customer != null && !customer.Result.IdentityNoVerified)
                    throw new ApiException(GenericEnums.BusinessErrorCode, "Kimlik bilgisi doğrulanmamış müşteriyle işlem yapmaya çalışıyorsunuz!");

                var hash = new HashPayment()
                {
                    CustomerId = customer.Result.Id.ToString(),
                    FailUrl = "www.payzee.com/fail-url",
                    OkUrl = "www.payzee.com/success-url",
                    HashPassword = _config.ApiKey,
                    OrderId = transaction.OrderId.ToString(),
                    Rnd = "1234",
                    TotalAmount = "1",
                    TxnType = GenericEnums.TxnType.Auth.ToString(),
                    UserCode = "test"
                };

                var vposRequest = VposPaymentRequestFactory.CreateNewInstance(transaction, customer.Result, hash, int.Parse(_config.MerchantId), int.Parse(_config.MemberId));

                paymentResponse = await _httpService.Post<VposResponse>(_config.PaymentUrl, vposRequest);


            }
            catch (ApiException ex)
            {
                if (ex.Code == GenericEnums.BusinessErrorCode)
                {
                    responseCode = ex.Code;
                    responseMessage = ex.Message;
                }
                else
                {
                    responseCode = GenericEnums.TechnicalErrorCode;
                    responseMessage = ex.InnerException.Message;
                }
                fail = true;
                statusCode = 500;
            }

            return await Task.FromResult(new ApiResponse<VposResponse> { Count = 0, ResponseCode = responseCode, ResponseMessage = responseMessage, Fail = fail, StatusCode = statusCode, Result = paymentResponse });
        }

        public Task<ApiResponse<VposResponse>> Void(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
