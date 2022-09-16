namespace Mycinema.API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Details { get; private set; }

        public CodeErrorException(int statusCode, string? message, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }        
    }
}
