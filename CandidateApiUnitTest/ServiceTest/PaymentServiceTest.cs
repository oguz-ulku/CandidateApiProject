using CandidateApiProject.Interface;
using CandidateApiProject.Models;
using CandidateApiProject.Services;
using CandidateApiUnitTest.Mock;
using Microsoft.Extensions.Options;
using Moq;


namespace CandidateApiUnitTest.ServiceTest
{
    public class PaymentServiceTest
    {
        [Fact]
        public async void Test_Sale_ReturnSuccess()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var httpServiceMock = new Mock<IHttpService>();
            var configMock = new Mock<IOptions<ApplicationSettings>>();

            configMock.Setup(_ => _.Value).Returns(PaymentMockData.Get_Application_Setting());

            customerServiceMock.Setup(_ => _.GetCustomerById("1")).ReturnsAsync(CustomerMockData.Get_Customer_Response_ReturnSuccess());

            httpServiceMock.Setup(_ =>  _.Post<VposResponse>(It.IsAny<string>(), It.IsAny<VposRequest>())).ReturnsAsync(PaymentMockData.Get_Vpos_Response_ReturnSuccess());

            var paymentService = new PaymentService(customerServiceMock.Object, configMock.Object, httpServiceMock.Object);

            var result = await paymentService.Sale(TransactionMockData.Get_Transaction_Request_ReturnSuccess());

            Assert.NotNull(result);
            Assert.Equal(GenericEnums.SuccessCode, result.ResponseCode);
            Assert.Equal(GenericEnums.SuccessMessage, result.ResponseMessage);
            Assert.Equal(PaymentMockData.Get_Vpos_Response_ReturnSuccess().ResponseCode, result.Result.ResponseCode);
            Assert.Equal(PaymentMockData.Get_Vpos_Response_ReturnSuccess().ResponseCode, result.Result.ResponseCode);
        }

        [Fact]
        public async void Test_Sale_ReturnError_WhenPaymentFail()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var httpServiceMock = new Mock<IHttpService>();
            var configMock = new Mock<IOptions<ApplicationSettings>>();

            configMock.Setup(_ => _.Value).Returns(PaymentMockData.Get_Application_Setting());
            

            customerServiceMock.Setup(_ => _.GetCustomerById("1")).ReturnsAsync(CustomerMockData.Get_Customer_Response_ReturnSuccess());

            httpServiceMock.Setup(_ => _.Post<VposResponse>(It.IsAny<string>(), It.IsAny<VposRequest>())).ReturnsAsync(PaymentMockData.Get_Vpos_Response_ReturnError_WhenPaymentFail());
         
            var paymentService = new PaymentService(customerServiceMock.Object, configMock.Object, httpServiceMock.Object);

            var result = await paymentService.Sale(TransactionMockData.Get_Transaction_Request_ReturnSuccess());

            Assert.NotNull(result);
            Assert.Equal(PaymentMockData.Get_Vpos_Response_ReturnError_WhenPaymentFail().ResponseCode, result.Result.ResponseCode);
            Assert.Equal(PaymentMockData.Get_Vpos_Response_ReturnError_WhenPaymentFail().ResponseMessage, result.Result.ResponseMessage);

        }
    }
}
