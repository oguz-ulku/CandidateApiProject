namespace CandidateApiProject.Models
{
    public class GenericEnums
    {
        public static readonly string SuccessCode = "00";
        public static readonly string BusinessErrorCode = "BE-101";
        public static readonly string TechnicalErrorCode = "TE-101";
        public static readonly string SuccessMessage = "İşlem Başarılı";
        public static readonly string BusinessErrorMessage = "I got a business error. Message -> {0}";
        public static readonly string TechnicalErrorMessage = "I got a technical error. Exception message -> {0}";
        public enum RecordStatus
        {
            Active = 1,
            Passive = 0
        }

        public enum Currency
        {
            TL = 949,
            USD = 950,
            EUR = 951
        }

        public enum TxnType
        {
            Auth,
            PreAuth,
            Void,
            ThreeD
        }
    }
}
