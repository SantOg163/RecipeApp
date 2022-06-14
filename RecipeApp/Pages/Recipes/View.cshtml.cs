using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;


namespace RecipeApp.Pages.Recipes
{
    public class ViewModel : PageModel
    {
        public RecipeDetail Recipe { get; set; }
        public bool CanEdit { get; set; }
        private readonly RecipeService _service;
        public IAuthorizationService _authService;
        public ViewModel(RecipeService service, IAuthorizationService authService)
        {
            _service = service;
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _service.GetRecipeDetail(id);
            if(Recipe == null )
                return NotFound();
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
