using FullstackTest.Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using FullstackTest.Front.DTOs.Response;


namespace FullstackTest.Front.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public LoginRequest LoginRequest { get; set; }

    public string ErrorMessage { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            ErrorMessage = "Dados inválidos.";
            return Page();
        }

        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:44396/");

        var json = JsonSerializer.Serialize(LoginRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("auth/signin", content);

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = "Usuário ou senha inválidos.";
            return Page();
        }

        var responseContent = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseContent);
        var root = doc.RootElement.GetProperty("token");

        var result = new AuthResponse
        {
            Token = root.GetProperty("token").GetString(),
            Username = root.GetProperty("username").GetString(),
            UserId = root.GetProperty("userId").GetGuid()
        };

        HttpContext.Session.SetString("Token", result.Token);
        //TempData["Token"] = result?.Token;

        return RedirectToPage("/Index");
    }
}
