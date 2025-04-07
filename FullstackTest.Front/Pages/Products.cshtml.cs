using FullstackTest.Front.DTOs.Request;
using FullstackTest.Front.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace FullstackTest.Front.Pages;

public class ProductsModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public ProductsModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [BindProperty]
    public ProductRequest ProductRequest { get; set; } = new();

    public List<ProductResponse> Products { get; set; } = new();

    public async Task OnGetAsync()
    {
        var token = HttpContext.Session.GetString("Token");
        //var token = TempData["Token"]?.ToString();
        if (string.IsNullOrEmpty(token)) return;

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var apiUrl = _configuration["ApiSettings:BaseUrl"] + "/Product";
        var response = await client.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<ProductResponse>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (products != null)
                Products = products;
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token)) return RedirectToPage("/Login");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var apiUrl = _configuration["ApiSettings:BaseUrl"] + "/Product";
        var content = new StringContent(JsonSerializer.Serialize(ProductRequest), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
            return RedirectToPage(); 

        TempData["Erro"] = "Erro ao salvar produto.";
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid id)
    {
        var token = HttpContext.Session.GetString("Token");

        if (string.IsNullOrEmpty(token)) return RedirectToPage("/Login");

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var apiUrl = _configuration["ApiSettings:BaseUrl"] + $"/Product/{id}";
        var response = await client.DeleteAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
            TempData["Erro"] = "Erro ao excluir produto.";

        return RedirectToPage(); // Atualiza a listagem
    }

}
