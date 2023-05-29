using CandidateApiProject.Models;
using CandidateApiProject.Utils;


namespace CandidateApiUnitTest.Mock
{
    public static class PaymentMockData
    {

        public static VposRequest Get_Vpos_Request_ReturnSuccess()
        {
            var hashPayment = new HashPayment()
            {
                CustomerId = "1",
                FailUrl = "www.payzee.com/fail-url",
                OkUrl = "www.payzee.com/success-url",
                HashPassword = "1234",
                OrderId = "1234",
                Rnd = "1234",
                TotalAmount = "1",
                TxnType = GenericEnums.TxnType.Auth.ToString(),
                UserCode = "test"
            };

            return new VposRequest
            {
                CardNumber = "4355084355084358",
                CVV = "000",
                ExpiryDateYear = "2026",
                ExpiryDateMonth = "12",
                CardAlias = "Visa",
                CardHolderName = "Akbank",

                MerchantId = 1,
                MemberId = 1,
                OrderId = "1234",
                Currency = ((int)GenericEnums.Currency.TL).ToString(),
                RequestIp = "192.168.1.1",
                CustomerId = "1",
                Hash = Helper.PaymentHash(hashPayment),
                InstallmentCount = "1",
                PointAmount = "0",
                TotalAmount = "1",
                TxnType = GenericEnums.TxnType.Auth.ToString(),
                UserCode = "test",
            };
        }

        public static VposResponse Get_Vpos_Response_ReturnSuccess()
        {
            return new VposResponse 
            { 
                 AuthCode = "test",
                 BankOrderId = "1",
                 BankResponseMessage = "test",
                 HideResponseTarget = true,
                 HostReference = "1",
                 InstallmentCount = 1,
                 OrderId= "1",
                 PaymentSystem = "test",
                 ResponseCode = "00",
                 ResponseHash = "1",
                 ResponseMessage = "İşlem Başarılı",
                 SaleDate = 1,
                 TotalAmount= "1",
                 TxnStatus = "1",
                 TxnType = "1",
                 Url ="1",
                 VposId = 1,
                 VposName = "test",
            };
        }

        public static VposResponse Get_Vpos_Response_ReturnError_WhenPaymentFail()
        {
            return new VposResponse
            {
                AuthCode = "test",
                BankOrderId = "1",
                BankResponseMessage = "test",
                HideResponseTarget = true,
                HostReference = "1",
                InstallmentCount = 1,
                OrderId = "1",
                PaymentSystem = "test",
                ResponseCode = "99",
                ResponseHash = "1",
                ResponseMessage = "Başarısız",
                SaleDate = 1,
                TotalAmount = "1",
                TxnStatus = "1",
                TxnType = "1",
                Url = "1",
                VposId = 1,
                VposName = "test",
            };
        }

        public static ApplicationSettings Get_Application_Setting()
        {
            return  new ApplicationSettings
            {
                ApiKey = "test",
                Email = "test",
                Lang = "test",
                MemberId = "1",
                MerchantId = "1",
                Password = "test",
                PaymentUrl = "",
                TokenUrl = "test"
            };
        }
    }
}
