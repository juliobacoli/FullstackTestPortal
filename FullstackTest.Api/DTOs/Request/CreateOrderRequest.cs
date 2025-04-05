namespace FullstackTest.Api.DTOs.Request;

public class CreateOrderRequest
{
    public List<OrderItemRequest> Products { get; set; } = new();
}

public class OrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; } 
}
