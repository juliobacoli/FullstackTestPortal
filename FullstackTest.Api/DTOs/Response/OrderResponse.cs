namespace FullstackTest.Api.DTOs.Response;

public class OrderResponse
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Total { get; set; }
}
