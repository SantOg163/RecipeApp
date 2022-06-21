using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public UpdateRecipeCommand Input { get; set; }
        private readonly RecipeService _service;
        public IAuthorizationService _authService;
        public bool CanEdit { get; set; }
        public EditModel(RecipeService service, IAuthorizationService authService)
        {
            _service = service;
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Input = await _service.GetRecipeForUpdate(id);
            if (Input == null)
                return NotFound();
            var recipe = await _service.GetRecipe(id);
            var isAuthorised = await _authService.AuthorizeAsync(User, recipe, "CanManageRecipe");
            CanEdit = isAuthorised.Succeeded;
            if (CanEdit == false)
            {
                return new ForbidResult();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                    await _service.UpdateRecipe(Input);
                return RedirectToPage("View", new {id=Input.Id});
            }
            catch(Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occured saving the recipe");
            }
        return Page();
        }
    }
}
