using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarvedRock.UI.Pages;

[AllowAnonymous]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
