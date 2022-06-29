using CarvedRock.Shared.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CarvedRock.UI.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<ProductModel>? Products { get; set; }

        public ProductsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("cr_client");
            Products = await client.GetFromJsonAsync<List<ProductModel>>("api/Products");
        }
    }
}
