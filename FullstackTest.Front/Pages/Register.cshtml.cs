using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using FullstackTest.Front.DTOs.Response;
using FullstackTest.Front.DTOs.Request;

namespace FullstackTest.Front.Pages;

public class RegisterModel : PageModel
{
    [BindProperty]
    public RegisterUserRequest RegisterRequest { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:44396/");

        var json = JsonSerializer.Serialize(RegisterRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("auth/signup", content);

        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = "Falha ao registrar usuário.";
            return Page();
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<AuthResponse>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        TempData["Token"] = result?.Token;

        return RedirectToPage("/Index");
    }

}
