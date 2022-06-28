using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarvedRock.UI.Pages;

public class SignoutModel : PageModel
{
    public IActionResult OnGet()
    {
        return SignOut("Cookies", "oidc");
    }
}


