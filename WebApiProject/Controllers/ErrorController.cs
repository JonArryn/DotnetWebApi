using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Utilities;

namespace WebApiProject.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("error-development")]
    public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
    {
        if (!hostEnvironment.IsDevelopment()) return NotFound();
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
        var exception = exceptionHandlerFeature.Error;

        if (exception is AppException appException)
            return Problem(
                title: exceptionHandlerFeature.Error.Message,
                detail: exceptionHandlerFeature.Error.StackTrace,
                statusCode: appException.StatusCode,
                instance: exceptionHandlerFeature.Error.Source,
                type: exceptionHandlerFeature.Error.GetType().Name
            );

        return Problem(
            title: $"An unexpected error occurred. Message: {exceptionHandlerFeature.Error.Message}",
            statusCode: StatusCodes.Status500InternalServerError,
            detail: exceptionHandlerFeature.Error.StackTrace,
            instance: exceptionHandlerFeature.Error.Source,
            type: exceptionHandlerFeature.Error.GetType().Name
        );
    }

    [Route("/error")]
    public IActionResult HandleError([FromServices] IHostEnvironment hostEnvironment)
    {
        var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature?.Error;

        if (exception == null)
            return Problem();

        if (exception is AppException appException)
            return Problem(
                title: "An error occurred",
                detail: appException.Message,
                statusCode: appException.StatusCode
            );

        return Problem(
            title: "An unexpected error occurred",
            statusCode: StatusCodes.Status500InternalServerError
        );
    }
}