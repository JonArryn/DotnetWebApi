Here's a `README.md` snippet with clear structure and formatting for documenting the manual process of creating a new **feature or resource (i.e., endpoint)** in your ASP.NET Core project. This is written to be copy-paste-ready into your repo with rich markdown syntax:

---

# 🚀 How to Add a New Feature or Resource (Endpoint)

Until we implement automation for scaffolding new resources, follow the steps below to manually create a new API endpoint in this project.

---

## 📦 1. Define the Entity

Create a new class in the `Entities` folder that represents your domain model.

```csharp
// Example: Entities/Product.cs
public class Product
{
    [Required] public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

---

## 🏗️ 2. Create a Migration and Apply It

Run the following commands in your CLI:

```bash
dotnet ef migrations add "AddProductEntity"
dotnet ef database update
```

This updates your database schema based on your new entity.

---

## 📁 3. Create the Repository

1. Add a concrete repository class in `Repositories/`.
2. Add an interface in `Contracts/Repositories/`.

```csharp
// Contracts/Repositories/IProductRepository.cs
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task DeleteAsync(int id);
}

// Repositories/ProductRepository.cs
public class ProductRepository : IProductRepository
{
    // Your EF Core data access logic here
}
```

---

## 🧠 4. Create the Service

1. Add a concrete service class in `Services/`.
2. Add an interface in `Contracts/Services/`.

```csharp
// Contracts/Services/IProductService.cs
public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<ProductResponse?> GetProductByIdAsync(int id);
    Task CreateProductAsync(ProductRequest request);
}

// Services/ProductService.cs
public class ProductService : IProductService
{
    // Your business logic here
}
```

---

## 🧾 5. Create DTOs (Data Transfer Objects)

Define the input/output shapes in:

* `DataTransfers/Requests/`
* `DataTransfers/Responses/`

```csharp
// Requests/ProductRequest.cs
public class ProductRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Responses/ProductResponse.cs
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

---

## 🔀 6. Set Up AutoMapper Mappings

Add a mapping profile in the `Mappings/` folder:

```csharp
// Mappings/ProductMapping.cs
public class ProductMapping : Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductResponse>();
        CreateMap<ProductRequest, Product>();
    }
}
```

---

## 📡 7. Create the Controller

Define your endpoint logic in a controller under `Controllers/`.

```csharp
// Controllers/ProductController.cs
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllProductsAsync());
}
```

---

## 🧩 8. Register Services and Repositories in the DI Container

Update the appropriate file in the `Extensions/` folder:

```csharp
// Extensions/ServiceCollectionExtensions.cs
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
```

Then, call the extension in `Program.cs`:

```csharp
builder.Services.AddProjectDependencies();
```

---

## ✅ Summary

Here’s the checklist to create a new resource:

* [ ] Entity in `Entities/`
* [ ] Migration created and applied
* [ ] Repository + interface
* [ ] Service + interface
* [ ] Request/Response DTOs
* [ ] AutoMapper mapping
* [ ] Controller
* [ ] DI registration in `Extensions/`

Once automation is ready, this process will be simplified significantly. For now, follow this guide to ensure consistency and clean architecture.
