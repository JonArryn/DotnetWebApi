namespace WebApiProject.Utilities;

public class HttpResponseException : Exception
{
    public HttpResponseException(int statusCode, object? value = null)
    {
        (StatusCode, Value) = (statusCode, value);
    }

    public int StatusCode { get; set; }
    public object? Value { get; set; }
}

public class EntityNotFoundException : HttpResponseException
{
    public EntityNotFoundException(object? value) : base(StatusCodes.Status404NotFound, value)
    {
    }
}