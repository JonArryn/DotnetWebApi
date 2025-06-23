using WebApiProject.Contracts.Repositories;
using WebApiProject.Contracts.Services;

namespace WebApiProject.Services;

public class TenantProviderService : ITenantProviderService
{
    private const string TenantIdHeaderName = "X-TenantId";
    private readonly IHttpContextAccessor _httpContextAccessor;
    // Default tenant ID for migrations
    private static readonly Guid DesignTimeTenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public TenantProviderService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetTenantId()
    {
        // During migrations, return a default tenant ID
        if (IsDesignTime())
        {
            return DesignTimeTenantId;
        }
        
        var tenantIdHeader = _httpContextAccessor.HttpContext?
            .Request
            .Headers[TenantIdHeaderName];
        
        
        if (!tenantIdHeader.HasValue || string.IsNullOrEmpty(tenantIdHeader.Value.ToString()))
        {
            throw new ApplicationException("Tenant ID is not present");
        }

        Guid tenantId;
        
        if (!Guid.TryParse(tenantIdHeader.Value.ToString(), out tenantId))
        {
            throw new ApplicationException("Tenant ID is invalid");
        }
        
        return tenantId;
    }
    
    public bool IsDesignTime()
    {
        // No HttpContext means we're likely in design time (migrations)
        return _httpContextAccessor.HttpContext == null;
    }
}