using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Pages.Recipes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public CreateRecipeCommand Input { get; set; }
        private readonly RecipeService _service;
        private readonly UserManager<ApplicationUser> _authService;
        public CreateModel(RecipeService service, UserManager<ApplicationUser> authService)
        {
            _service = service; 
            _authService = authService;
        }
        public void OnGet()
        {
            Input = new CreateRecipeCommand();
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _authService.GetUserAsync(User);
                    var id = await _service.CreateRecipe(Input,user);
                    return RedirectToPage("View", new { id = id });
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occured saving the recipe");
                Console.WriteLine(ModelState.ErrorCount);
            }
            return Page();
        }
    }
}
