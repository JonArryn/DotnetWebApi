using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace WebApiProject.Controllers;

public class AppBaseController< TService> : ControllerBase
{

    protected readonly TService Service;
        
    public AppBaseController( TService service)
    {
        Service = service;
        
    }

    protected Guid GetUserId()
    {
        Guid userId;
        // Extract user ID from claims
        if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId))
        {
            // TODO: Likely need to throw an error here instead of returning null
            throw new UnauthorizedAccessException();
        }

        return userId;
    }
}