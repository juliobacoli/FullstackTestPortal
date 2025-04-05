using FullstackTest.Api.DTOs;
using FullstackTest.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullstackTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var id = await _productService.CreateAsync(request);
        return Ok(new { id });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
    {
        var updated = await _productService.UpdateAsync(id, request);
        if (!updated)
            return NotFound(new { message = "Produto não encontrado." });

        return Ok(new { message = "Produto atualizado com sucesso." });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _productService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "Produto não encontrado." });

        return Ok(new { message = "Produto excluído com sucesso." });
    }
}
