using CandidateApiProject.Interface;
using CandidateApiProject.Models;
using CandidateApiProject.Services;
using CandidateApiUnitTest.Mock;
using FluentNHibernate.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace CandidateApiUnitTest.ServiceTest
{
    public class CustomerServiceTest
    {

        public CustomerServiceTest()
        {
            
        }

        [Fact]
        public async void Test_Create_Customer_ReturnSuccess()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var serviceSessionMock = new Mock<IServiceSession>();
            var firstTime = true;
            serviceSessionMock.Setup(_ => _.Alls<Customer>()).Returns(() =>
            {
                if (!firstTime)
                    return CustomerMockData.Get_Customer_List();
                
                firstTime = false;
                return CustomerMockData.Get_Customer_List_ReturnEmpty();
            });
       
            var TCKNAuthenticationServiceMock = new Mock<TCKNAuthenticationService>();
            customerServiceMock.Setup(_ => _.CreateCustomer(CustomerMockData.Get_Customer_Request_Success())).ReturnsAsync(CustomerMockData.Get_Customer_Response_ReturnSuccess());

            var customerService = new CustomerService(serviceSessionMock.Object, TCKNAuthenticationServiceMock.Object);

            var result = await customerService.CreateCustomer(CustomerMockData.Get_Customer_Request_Success());

            Assert.NotNull(result);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().ResponseCode, result.ResponseCode);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().Fail, result.Fail);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().Count, result.Count);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().StatusCode, result.StatusCode);
        }

        [Fact]
        public async void Test_Create_Customer_ReturnFail_WhenIdentityNoNotVerify()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var serviceSessionMock = new Mock<IServiceSession>();
            var firstTime = true;
            serviceSessionMock.Setup(_ => _.Alls<Customer>()).Returns(() =>
            {
                if (!firstTime)
                    return CustomerMockData.Get_Customer_List();

                firstTime = false;
                return CustomerMockData.Get_Customer_List_ReturnEmpty();
            });

            var TCKNAuthenticationServiceMock = new Mock<TCKNAuthenticationService>();
            customerServiceMock.Setup(_ => _.CreateCustomer(CustomerMockData.Get_Customer_Request_ReturnError())).ReturnsAsync(CustomerMockData.Get_Customer_Response_ReturnError_WhenCheckIdentityNoNotVerify());

            var customerService = new CustomerService(serviceSessionMock.Object, TCKNAuthenticationServiceMock.Object);

            var result = await customerService.CreateCustomer(CustomerMockData.Get_Customer_Request_ReturnError());

            Assert.NotNull(result);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCheckIdentityNoNotVerify().ResponseCode, result.ResponseCode);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCheckIdentityNoNotVerify().Fail, result.Fail);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCheckIdentityNoNotVerify().Count, result.Count);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCheckIdentityNoNotVerify().StatusCode, result.StatusCode);
        }

        [Fact]
        public async void Test_Get_Customer_By_Id_ReturnSuccess()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var serviceSessionMock = new Mock<IServiceSession>();
            serviceSessionMock.Setup(_ => _.Alls<Customer>()).Returns(CustomerMockData.Get_Customer_List());

            var TCKNAuthenticationServiceMock = new Mock<TCKNAuthenticationService>();
            customerServiceMock.Setup(_ => _.GetCustomerById("1")).ReturnsAsync(CustomerMockData.Get_Customer_Response_ReturnSuccess());

            var customerService = new CustomerService(serviceSessionMock.Object, TCKNAuthenticationServiceMock.Object);

            var result = await customerService.GetCustomerById("1");

            Assert.NotNull(result);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().ResponseCode, result.ResponseCode);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().Fail, result.Fail);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().Count, result.Count);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnSuccess().StatusCode, result.StatusCode);
        }

        [Fact]
        public async void Test_Get_Customer_By_Id_ReturnError_WhenCustomerNotFound()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var serviceSessionMock = new Mock<IServiceSession>();
            serviceSessionMock.Setup(_ => _.Alls<Customer>()).Returns(CustomerMockData.Get_Customer_List());

            var TCKNAuthenticationServiceMock = new Mock<TCKNAuthenticationService>();
            customerServiceMock.Setup(_ => _.GetCustomerById("1")).ReturnsAsync(CustomerMockData.Get_Customer_Response_ReturnError_WhenCustomerNotFound());

            var customerService = new CustomerService(serviceSessionMock.Object, TCKNAuthenticationServiceMock.Object);

            var result = await customerService.GetCustomerById("3");

            Assert.NotNull(result);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCustomerNotFound().ResponseCode, result.ResponseCode);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCustomerNotFound().Fail, result.Fail);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCustomerNotFound().Count, result.Count);
            Assert.Equal(CustomerMockData.Get_Customer_Response_ReturnError_WhenCustomerNotFound().StatusCode, result.StatusCode);
        }
    }
}