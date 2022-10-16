using RecipeApp.Models;
using RecipeApp.Data;
using Microsoft.EntityFrameworkCore;
//update-database в диспетчере пакетов
namespace RecipeApp
{ 
    public class RecipeService
    {
        readonly AppDbContext _context;
        public RecipeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DoesRecipeExistAsync(int id)
        {
            return await _context.Recipes.Where(r=>!r.IsDeleted && r.RecipeId == id).AnyAsync();
        }
        public async Task<List<RecipeSummary>> GetRecipes()
        {
            return await _context.Recipes.Where(recipe => !recipe.IsDeleted).Select(recipe => new RecipeSummary
            {
                Id = recipe.RecipeId,
                Name = recipe.Name,
                TimeToCook = $"{recipe.TimeToCook.Hours}hrs {recipe.TimeToCook.Minutes}mins"
            })
                .ToListAsync();
        }
        public async Task<List<RecipeSummary>> GetRecipesForUser(string? userId,int count)
        {
            return await _context.Recipes.Where(recipe => !recipe.IsDeleted && recipe.CreatedById==userId).Take(count).Select(recipe => new RecipeSummary
            {
                Id = recipe.RecipeId,
                Name = recipe.Name,
                TimeToCook = $"{recipe.TimeToCook.Hours}hrs {recipe.TimeToCook.Minutes}mins"
            })
                .ToListAsync();
        }
        public async Task<RecipeDetail> GetRecipeDetail(int id)
        {
            return await _context.Recipes.Where(recipe => recipe.RecipeId == id && !recipe.IsDeleted).Select(recipe => new RecipeDetail
            {
                Id = recipe.RecipeId,
                Name = recipe.Name,
                Method = recipe.Method,
                Items = recipe.ingredients.Select(item => new RecipeDetail.Item { Name = item.Name, Quantity = $"{item.Quantity} {item.Unit}" })
            }).SingleOrDefaultAsync();
        }
        public async Task<Recipe> GetRecipe(int id)
        {
            return await _context.Recipes.Where(recipe => recipe.RecipeId == id && !recipe.IsDeleted).FirstAsync();
        }
        public async Task<UpdateRecipeCommand> GetRecipeForUpdate(int id)
        {
            return await _context.Recipes.Where(recipe => recipe.RecipeId == id && !recipe.IsDeleted).Select(recipe => new UpdateRecipeCommand
            {
                Id = recipe.RecipeId,
                Name = recipe.Name,
                TimeToCookHours = recipe.TimeToCook.Hours,
                TimeToCookMinutes = recipe.TimeToCook.Minutes,
                IsVegan = recipe.IsVegan,
                IsVegetarian = recipe.IsVegetarian,
            }).SingleOrDefaultAsync();
        }
        public async Task<int> CreateRecipe(CreateRecipeCommand cmd, ApplicationUser createdBy)
        {
            var recipe = cmd.ToRecipe(createdBy);
            _context.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe.RecipeId;
        }
        public async Task DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if(recipe == null)
                throw new Exception("Unable to find recipe");
            recipe.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRecipe(UpdateRecipeCommand cmd)
        {
            var recipe = await _context.Recipes.FindAsync(cmd.Id);
            if (recipe == null)
                throw new Exception("Unable to find recipe");
            if (recipe.IsDeleted)
                throw new Exception("Unable to update deleted recipe");
            cmd.UpdateRecipe(recipe);
            await _context.SaveChangesAsync();
        }
    }
}
