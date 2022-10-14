using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeApp.Data;
using RecipeApp.Models;
using RecipeApp.Pages;

namespace RecipeApp.ViewComponents
{
    public class MyRecipesViewComponent : ViewComponent
    {
        private readonly RecipeService _service;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public IEnumerable<RecipeSummary> Recipes { get; private set; }
        public MyRecipesViewComponent(RecipeService service, ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int numberOfRecipe)
        {
            if (!User.Identity.IsAuthenticated)
                return View("Unauthenticated");
            var userId = _userManager.GetUserId(HttpContext.User);
            var recipes = _service.GetRecipesForUser(userId, numberOfRecipe);
            return View(recipes);
        }
    }
}