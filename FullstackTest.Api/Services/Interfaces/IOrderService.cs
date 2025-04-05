using FullstackTest.Api.DTOs.Request;
using FullstackTest.Api.DTOs.Response;

namespace FullstackTest.Api.Services.Interfaces;

public interface IOrderService
{
    Task<Guid> CreateAsync(Guid userId, CreateOrderRequest request);
    Task<IEnumerable<OrderResponse>> GetAllByUserAsync(Guid userId, decimal? min, decimal? max, DateTime? start, DateTime? end);
    Task<OrderDetailsResponse?> GetDetailsByIdAsync(Guid orderId, Guid userId);
}