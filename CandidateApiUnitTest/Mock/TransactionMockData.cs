using CandidateApiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApiUnitTest.Mock
{
    public static class TransactionMockData
    {
        public static Transaction Get_Transaction_Request_ReturnSuccess()
        {
            return new Transaction 
            { 
                Amount = "1",
                CardPan = "1234",
                CustomerId = 1,
                Id = 1,
                OrderId = 1,
                ResponseCode = "00",
                ResponseMessage = "İşlem Başarılı",
                Status = true,
                TransactionId = 1,
                TypeId = "1",
                
            };
        }
    }
}
