using FullstackTest.Api.DTOs.Request;
using FullstackTest.Api.DTOs.Response;

namespace FullstackTest.Api.Services.Interfaces;

public interface IProductService
{
    Task<Guid> CreateAsync(CreateProductRequest request);
    Task<bool> UpdateAsync(Guid id, UpdateProductRequest request);
    Task<bool> DeleteAsync(Guid id);
    Task<ProductResponse?> GetByIdAsync(Guid id);
}
