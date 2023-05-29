namespace CandidateApiProject.Exceptions
{
    public class ApiException : Exception
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public ApiException(): base() { }
        public ApiException(string message):base(message)
        {
            Message = message;
        }

        public ApiException(string message, Exception? innerException) : base(message, innerException)
        {
            Message = message;
        }

        public ApiException(string code, string message):base(message)
        {
            Code = code;
            Message = message;
        }
    }
}
