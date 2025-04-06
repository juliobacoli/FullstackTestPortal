using FullstackTest.Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


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

        return RedirectToPage("/Index");
    }
}
