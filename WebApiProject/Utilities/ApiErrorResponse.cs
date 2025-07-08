namespace WebApiProject.Utilities;

public enum ResponseStatus
{
    Success,
    Error,
    Fail
}

public class ApiErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public string? ErrorCode { get; set; }
    public ResponseStatus Status { get; set; } = ResponseStatus.Error;
    public object? Details { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Convenient static factory methods
    public static ApiErrorResponse NotFound(string message, string? errorCode = "B0001")
    {
        return new ApiErrorResponse
        {
            Message = message,
            ErrorCode = errorCode,
            Status = ResponseStatus.Fail
        };
    }

    public static ApiErrorResponse BadRequest(string message, string? errorCode = "B0004")
    {
        return new ApiErrorResponse
        {
            Message = message,
            ErrorCode = errorCode,
            Status = ResponseStatus.Fail
        };
    }

    public static ApiErrorResponse Conflict(string message, string? errorCode = "B0005")
    {
        return new ApiErrorResponse
        {
            Message = message,
            ErrorCode = errorCode,
            Status = ResponseStatus.Fail
        };
    }
}