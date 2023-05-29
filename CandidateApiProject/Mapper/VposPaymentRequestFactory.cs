using CandidateApiProject.Models;
using CandidateApiProject.Utils;

namespace CandidateApiProject.Mapper
{
    public static class VposPaymentRequestFactory
    {
        public static VposRequest CreateNewInstance(Transaction transaction, Customer customer, HashPayment hashPayment, int merchantId, int memberId)
        {
            var vposRequest = new VposRequest();

            vposRequest.CardNumber = "4355084355084358";
            vposRequest.CVV = "000";
            vposRequest.ExpiryDateYear = "2026";
            vposRequest.ExpiryDateMonth = "12";
            vposRequest.CardAlias = "Visa";
            vposRequest.CardHolderName = "Akbank";

            vposRequest.MerchantId = merchantId;
            vposRequest.MemberId = memberId;
            vposRequest.OrderId = transaction.OrderId.ToString();
            vposRequest.Currency = ((int)GenericEnums.Currency.TL).ToString();
            vposRequest.RequestIp = "192.168.1.1";
            vposRequest.CustomerId = customer.Id.ToString();


            vposRequest.Hash = Helper.PaymentHash(hashPayment);
            vposRequest.InstallmentCount = "1";
            vposRequest.PointAmount = "0";
            vposRequest.TotalAmount = transaction.Amount;
            vposRequest.TxnType = GenericEnums.TxnType.Auth.ToString();
            vposRequest.UserCode = "test";

            return vposRequest;
        }
    }
}
