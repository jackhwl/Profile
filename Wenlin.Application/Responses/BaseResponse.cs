namespace Wenlin.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
            NotFound = false;
        }
        public BaseResponse(string message)
        {
            Success = true;
            NotFound = false;
            Message = message;
        }
        public BaseResponse(string message, bool success, bool notFound)
        {
            Success = success;
            Message = message;
            NotFound = notFound;
        }

        public bool Success { get; set; }
        public bool NotFound { get; set; }
        public string Message { get; set; } = string.Empty;
        public IDictionary<string, IEnumerable<string>>? ValidationErrors { get; set; }
        public string ValidationErrorsString => ValidationErrors == null ? string.Empty : string.Join(";", ValidationErrors.Values);
    }
}
