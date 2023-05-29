using CandidateApiProject.Interface;
using CandidateApiProject.Models;

namespace CandidateApiProject.Services
{
    public class TransactionService : ITransactionService 
    {
        private readonly IServiceSession _serviceSession;
        private readonly IPaymentProcess _paymentProcess;
        public TransactionService(IServiceSession serviceSession, IPaymentProcess paymentProcess) 
        { 
            _serviceSession = serviceSession;
            _paymentProcess = paymentProcess;
        }
        public async Task<ApiResponse<Transaction>> CreateSaleTransaction(Transaction transaction)
        {
            var responseCode = GenericEnums.SuccessCode;
            var responseMessage = GenericEnums.SuccessMessage;
            var fail = false;
            var statusCode = 200;
            var count = 0;

            try
            {
                var saleResponse = await _paymentProcess.Sale(transaction);

                if (saleResponse != null && "00".Equals(saleResponse.ResponseCode))
                {
                    transaction.ResponseCode = saleResponse.ResponseCode;
                    transaction.ResponseMessage = saleResponse.ResponseMessage;
                    count = 1;
                    transaction.Status = true;
                }
                else
                {
                    transaction.ResponseCode = "99";
                    transaction.ResponseMessage = "Satış işlemi başarısız gerçekleşti!";
                    responseCode = saleResponse.ResponseCode;
                    responseMessage = saleResponse.ResponseMessage;
                    fail = true;
                    statusCode = 400;
                }

            }
            catch (Exception ex)
            {
                responseCode = GenericEnums.TechnicalErrorCode;
                responseMessage = ex.Message;
                fail = true;
                statusCode = 500;
            }
            finally
            {
                _serviceSession.Save<Transaction>(transaction);
            }

            return await Task.FromResult(new ApiResponse<Transaction> { Count = count, ResponseCode = responseCode, ResponseMessage = responseMessage, Fail = fail, StatusCode = statusCode, Result = transaction });
        }

        public Task<ApiResponse<bool>> Void(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
