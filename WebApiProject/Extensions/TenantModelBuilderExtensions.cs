using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Contracts.Services;
using WebApiProject.Entities;

namespace WebApiProject.Extensions;

public static class TenantModelBuilderExtensions
{
    public static void ApplyTenantConfiguration(this ModelBuilder modelBuilder, ITenantProviderService tenantProvider, ILogger logger)
    {
        var isDesignTime = tenantProvider.IsDesignTime();
        // Find all entity types that inherit from TenantBaseEntity
        var tenantEntityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(entityType => typeof(TenantBaseEntity).IsAssignableFrom(entityType.ClrType))
            .ToList();

        foreach (var entityType in tenantEntityTypes)
        {
            try
            {
                // Add index on TenantId for performance
                modelBuilder.Entity(entityType.ClrType)
                    .HasIndex(nameof(TenantBaseEntity.TenantId));
                
                // Skip filters in design-time but still create indexes
                if (isDesignTime)
                {
                    logger.LogInformation($"Running in design time, skipping query filter for {entityType.ClrType.Name}");
                    continue;
                }

                // The following code creates a filter: e => e.TenantId == tenantProvider.GetTenantId()

                // Add index on TenantId for performance
                modelBuilder.Entity(entityType.ClrType)
                    .HasIndex(nameof(TenantBaseEntity.TenantId));

                // 1. Create parameter "e" of the entity type
                var parameter = Expression.Parameter(entityType.ClrType, "e");
        
                // 2. Create property access: e.TenantId
                var tenantIdProperty = Expression.Property(parameter, nameof(TenantBaseEntity.TenantId));

                // 3. Create method call: tenantProvider.GetTenantId()
                var getTenantIdMethod = typeof(ITenantProviderService).GetMethod(nameof(ITenantProviderService.GetTenantId));
                if (getTenantIdMethod == null)
                {
                    throw new InvalidOperationException($"Method {nameof(ITenantProviderService.GetTenantId)} not found on {nameof(ITenantProviderService)}");
                }
            
                var tenantIdMethod = Expression.Call(
                    Expression.Constant(tenantProvider),
                    getTenantIdMethod);

                // 4. Create equality comparison: e.TenantId == tenantProvider.GetTenantId()
                var body = Expression.Equal(tenantIdProperty, tenantIdMethod);

                // 5. Create lambda expression: e => e.TenantId == tenantProvider.GetTenantId()
                var lambda = Expression.Lambda(body, parameter);

                // 6. Apply filter to entity type
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate
                logger.LogError(ex, $"Failed to apply tenant configurations in model builder extension {ex.Message}");
                throw new Exception($"Failed to apply tenant configuration for entity {entityType.ClrType.Name}", ex);
            }
            
        }
    }
    
    
}