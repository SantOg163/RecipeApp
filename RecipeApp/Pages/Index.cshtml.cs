using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;

namespace RecipeApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RecipeService _service;
        public IEnumerable<RecipeSummary> Recipes { get; private set; }
        public IndexModel(RecipeService service) => _service = service;
        public async Task OnGet() => Recipes = await _service.GetRecipes();
    }
}