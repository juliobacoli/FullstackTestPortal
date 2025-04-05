namespace FullstackTest.Api.DTOs.Response;

public class OrderDetailsResponse
{
    public Guid OrderId { get; set; }
    public decimal Total { get; set; }
    public List<OrderItemResponse> Items { get; set; } = new();
}

public class OrderItemResponse
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}