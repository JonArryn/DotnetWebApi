using static WebApiProject.Utilities.ErrorCatalog;

namespace WebApiProject.Utilities;

public class HttpResponseException : Exception
{
    public HttpResponseException(int statusCode, ApiErrorResponse response)
    {
        StatusCode = statusCode;
        Value = response;
    }

    public HttpResponseException(int statusCode, string message, string? errorCode = null)
        : this(statusCode, new ApiErrorResponse { Message = message, ErrorCode = errorCode })
    {
    }

    public int StatusCode { get; set; }
    public object? Value { get; set; }
}

public class EntityNotFoundException : HttpResponseException
{
    public EntityNotFoundException(string? message = "Not found", string? errorCode = BusinessErrorCodes.GENERIC_NOT_FOUND)
        : base(StatusCodes.Status404NotFound, ApiErrorResponse.NotFound(message, errorCode))
    {
    }
}

public class CouldNotAuthenticateException : HttpResponseException
{
    public CouldNotAuthenticateException(string? message = "Could not log in", string? errorCode = AuthErrorCodes.GENERIC_AUTH_ERROR)
        : base(StatusCodes.Status400BadRequest, ApiErrorResponse.BadRequest(message, errorCode))
    {
    }
}