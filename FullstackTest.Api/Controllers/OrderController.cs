using FullstackTest.Api.DTOs.Request;
using FullstackTest.Api.Models;
using FullstackTest.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FullstackTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var orderId = await _orderService.CreateAsync(userId, request);
        return Ok(new { orderId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] decimal? min,
        [FromQuery] decimal? max,
        [FromQuery] DateTime? start,
        [FromQuery] DateTime? end)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var orders = await _orderService.GetAllByUserAsync(userId, min, max, start, end);
        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetDetails(Guid orderId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var details = await _orderService.GetDetailsByIdAsync(orderId, userId);

        if (details == null)
            return NotFound(new { message = "Pedido não encontrado." });

        return Ok(details);
    }
}