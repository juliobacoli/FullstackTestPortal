using FullstackTest.Api.Data;
using FullstackTest.Api.DTOs.Request;
using FullstackTest.Api.DTOs.Response;
using FullstackTest.Api.Models;
using FullstackTest.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FullstackTest.Api.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(Guid userId, CreateOrderRequest request)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Items = new List<OrderItem>()
        };

        foreach (var item in request.Products)
        {
            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price,
            });
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order.Id;
    }

    public async Task<IEnumerable<OrderResponse>> GetAllByUserAsync(Guid userId, decimal? min, decimal? max, DateTime? start, DateTime? end)
    {
        var query = _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.Items)
            .AsQueryable();

        if (min.HasValue)
            query = query.Where(o => o.Items.Sum(i => i.Price * i.Quantity) >= min.Value);

        if (max.HasValue)
            query = query.Where(o => o.Items.Sum(i => i.Price * i.Quantity) <= max.Value);

        if (start.HasValue)
            query = query.Where(o => o.CreatedAt >= start.Value);

        if (end.HasValue)
            query = query.Where(o => o.CreatedAt <= end.Value);

        var result = await query
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new OrderResponse
            {
                OrderId = o.Id,
                CreatedAt = o.CreatedAt,
                Total = o.Items.Sum(i => i.Price * i.Quantity)
            })
            .ToListAsync();

        return result;
    }

    public async Task<OrderDetailsResponse?> GetDetailsByIdAsync(Guid orderId, Guid userId)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

        if (order == null)
            return null;

        return new OrderDetailsResponse
        {
            OrderId = order.Id,
            Total = order.Items.Sum(i => i.Price * i.Quantity),
            Items = order.Items.Select(i => new OrderItemResponse
            {
                ProductName = i.Product.Name,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }
}
