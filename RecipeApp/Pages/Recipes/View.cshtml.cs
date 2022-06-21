using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;


namespace RecipeApp.Pages.Recipes
{
    public class ViewModel : PageModel
    {
        public RecipeDetail Recipe { get; set; }
        private readonly ILogger<ViewModel> _logger;
        public bool CanEdit { get; set; }
        private readonly RecipeService _service;
        public IAuthorizationService _authService;
        public ViewModel(RecipeService service, IAuthorizationService authService, ILogger<ViewModel> logger)
        {
            _service = service;
            _authService = authService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _service.GetRecipeDetail(id);
            _logger.LogInformation($"Loading recipe with id {id}");
            if(Recipe == null)
            {
                _logger.LogWarning($"Could not find recipe with id {id}");
                return NotFound();
            }
            var recipe = await _service.GetRecipe(id);
            var isAuthorised = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            CanEdit = isAuthorised.Succeeded;
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteRecipe(id);
            return RedirectToPage("/Index");
        }
    }
}
