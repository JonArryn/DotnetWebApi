namespace WebApiProject.Contracts.Services;

public interface ITenantProviderService
{
    public Guid GetTenantId();
    bool IsDesignTime();
}