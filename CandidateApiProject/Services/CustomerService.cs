using CandidateApiProject.Exceptions;
using CandidateApiProject.Interface;
using CandidateApiProject.Models;
using Microsoft.AspNetCore.Http;
using static CandidateApiProject.Models.GenericEnums;

namespace CandidateApiProject.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IServiceSession _serviceSession;
        private readonly TCKNAuthenticationService _authenticationService;
        
        public CustomerService(IServiceSession serviceSession, TCKNAuthenticationService authenticationService) 
        {
            _serviceSession = serviceSession;
            _authenticationService = authenticationService;
        }

        public async Task<ApiResponse<Customer>> CreateCustomer(Customer request)
        {
            var responseCode = GenericEnums.SuccessCode;
            var responseMessage = GenericEnums.SuccessMessage;
            var fail = false;
            var statusCode = 200;
            var count = 0;
            Customer customer = null;
            try
            {

                if (!string.IsNullOrEmpty(request.IdentityNo) && request.IdentityNo.Length != 11)
                    throw new ApiException(GenericEnums.BusinessErrorCode, "TC kimlik no uzunluğu 11 haneli olmalıdır!");

                customer = new Customer();
                customer = _serviceSession.Alls<Customer>().Where(x => x.IdentityNo.Equals(request.IdentityNo)).FirstOrDefault();

                if(customer == null)
                {
                    request.IdentityNoVerified = Convert.ToBoolean(RecordStatus.Passive);
                    _serviceSession.Save<Customer>(request);
                }

                var checkIdentityNoResponse = await _authenticationService.CheckIdentityNo(request.IdentityNo, request.Name, request.Surname, Convert.ToDateTime(request.BirthDate));

                if (checkIdentityNoResponse)
                {
                    if(customer == null)
                        customer =  _serviceSession.Alls<Customer>().Where(x => x.IdentityNo.Equals(request.IdentityNo)).FirstOrDefault();

                    if(customer != null)
                    {
                        count = 1;
                        customer.IdentityNoVerified = Convert.ToBoolean(RecordStatus.Active);
                        _serviceSession.BeginTransaction();
                        _serviceSession.Update<Customer>(customer);
                        _serviceSession.Commit();
                    }
                    else
                    {
                        throw new ApiException(GenericEnums.BusinessErrorCode, "Müşteri tablosunda ilgili müşteri bulunamadı!");
                    }
                    
                }
                else
                {
                    responseCode = GenericEnums.BusinessErrorCode;
                    responseMessage = "TCKN servisinden kimlik numarası doğrulanamadı!";
                    fail = true;
                    statusCode = 400;
                }

                    
            }
            catch(ApiException ex)
            {
                if(ex.Code == GenericEnums.BusinessErrorCode)
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

            return await Task.FromResult(new ApiResponse<Customer>{Count = count, ResponseCode = responseCode, ResponseMessage = responseMessage, Fail = fail, StatusCode = statusCode, Result = customer });

        }

        public async Task<ApiResponse<Customer>> GetCustomerById(string id)
        {
            var responseCode = GenericEnums.SuccessCode;
            var responseMessage = GenericEnums.SuccessMessage;
            var fail = false;
            var statusCode = 200;
            var count = 0;
            Customer customer = null;
            try
            {
                customer = _serviceSession.Alls<Customer>().Where(x => x.Id.Equals(int.Parse(id))).FirstOrDefault();

                if (customer == null)
                    throw new ApiException(GenericEnums.BusinessErrorCode, "Müşteri bulunamadı!");

                count = 1;
            }
            catch (ApiException ex)
            {
                if(ex.Code == GenericEnums.BusinessErrorCode)
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

            return await Task.FromResult(new ApiResponse<Customer> { Count = count, ResponseCode = responseCode, ResponseMessage = responseMessage, Fail = fail, StatusCode = statusCode, Result = customer });
        }

    }
}
