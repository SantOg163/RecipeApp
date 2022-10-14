using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Data;
using RecipeApp.Models;
using System.Linq;

namespace RecipeApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RecipeService _service;
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<RecipeSummary> Recipes { get; private set; }
        public IndexModel(RecipeService service, ILogger<IndexModel> logger)
        {
            _service = service;
            _logger = logger;
        }
        public async Task OnGet()
        {
            Recipes = await _service.GetRecipes();
            _logger.LogInformation($"Loaded {Recipes.Count()} recipes");
        }
    }
}