using CandidateApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApiUnitTest.Mock
{
    public static class CustomerMockData
    {
        public static Customer Get_Customer_Request_Success()
        {
            return new Customer
            {
                BirthDate = "27/04/1993",
                IdentityNo = "22114752958",
                IdentityNoVerified = false,
                Name = "Oğuz",
                Surname = "Ülkü",
                Status = false
            };
        }

        public static Customer Get_Customer_Request_ReturnError()
        {
            return new Customer
            {
                BirthDate = "27/04/1993",
                IdentityNo = "22114752959",
                IdentityNoVerified = false,
                Name = "Oğuz",
                Surname = "Ülkü",
                Status = false
            };
        }

        public static ApiResponse<Customer> Get_Customer_Response_ReturnSuccess()
        {
            return new ApiResponse<Customer>
            {
                Count = 1,
                Fail = false,
                ResponseCode = "00",
                ResponseMessage = "İşlem Başarılı",
                Result = new Customer
                {
                    Id = 1,
                    BirthDate = "27/04/1993",
                    IdentityNo = "22114752958",
                    IdentityNoVerified = true,
                    Name = "Oğuz",
                    Surname = "Ülkü",
                    Status = true
                },
                StatusCode = 200
            };
        }

        public static ApiResponse<Customer> Get_Customer_Response_ReturnError_WhenCustomerNotFound()
        {
            return new ApiResponse<Customer>
            {
                Count = 0,
                Fail = true,
                ResponseCode = "BE-101",
                ResponseMessage = "Müşteri bulunamadı!",
                Result = new Customer
                {
                    Id = 1,
                    BirthDate = "27/04/1993",
                    IdentityNo = "22114752958",
                    IdentityNoVerified = true,
                    Name = "Oğuz",
                    Surname = "Ülkü",
                    Status = true
                },
                StatusCode = 500
            };
        }

        public static ApiResponse<Customer> Get_Customer_Response_ReturnError_WhenCheckIdentityNoNotVerify()
        {
            return new ApiResponse<Customer>
            {
                Count = 0,
                Fail = true,
                ResponseCode = "BE-101",
                ResponseMessage = "Müşteri tablosunda ilgili müşteri bulunamadı!",
                Result = new Customer
                {
                    BirthDate = "27/04/1993",
                    IdentityNo = "22114752959",
                    IdentityNoVerified = true,
                    Name = "Oğuz",
                    Surname = "Ülkü",
                    Status = true
                },
                StatusCode = 400
            };
        }

        public static List<Customer> Get_Customer_List_ReturnEmpty()
        {
            return new List<Customer>
            {
                new Customer
                {
                    BirthDate = "",
                    IdentityNo = "",
                    IdentityNoVerified = false,
                    Name = "",
                    Surname = "",
                    Status = false
                }
            };
        }

        public static List<Customer> Get_Customer_List()
        {
            return new List<Customer>
            {
               new Customer
                {
                   Id = 1,
                BirthDate = "27/04/1993",
                IdentityNo = "22114752958",
                IdentityNoVerified = false,
                Name = "Oğuz",
                Surname = "Ülkü",
                Status = false
                },
                new Customer
                {
                    Id = 2,
                BirthDate = "27/04/1993",
                IdentityNo = "22114752959",
                IdentityNoVerified = false,
                Name = "Oğuz",
                Surname = "Ülkü",
                Status = false
                }
            };
        }

    }
}
