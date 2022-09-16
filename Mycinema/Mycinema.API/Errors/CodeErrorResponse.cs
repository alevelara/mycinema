namespace Mycinema.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; private set; }
        public string? Message { get; private set; }

        public CodeErrorResponse(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Request with errors",
                401 => "Resource not authorized",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => string.Empty
            };
        }
    }
}
