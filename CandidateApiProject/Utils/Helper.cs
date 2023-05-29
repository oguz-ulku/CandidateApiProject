using CandidateApiProject.Models;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CandidateApiProject.Utils
{
    public static class Helper
    {
        public static string PaymentHash(HashPayment request)
        {
            var hashString = $"{request.HashPassword}{request.UserCode}{request.Rnd}{request.TxnType}{request.TotalAmount}{request.CustomerId}{request.OrderId}{request.OkUrl}{request.FailUrl}";
            var s512 = SHA512.Create();
            var byteConverter = new UnicodeEncoding();
            var bytes = s512.ComputeHash(byteConverter.GetBytes(hashString));
            var hash = BitConverter.ToString(bytes).Replace("-", "");
            return hash;
        }

    }
}
