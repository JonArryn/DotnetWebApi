namespace WebApiProject.Utilities;

public class AppException : Exception
{
    public AppException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}

public class EntityNotFoundException : AppException
{
    public EntityNotFoundException(string message, int statusCode) : base(message, statusCode)
    {
    }
}