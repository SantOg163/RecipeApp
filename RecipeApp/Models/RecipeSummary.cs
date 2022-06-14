using RecipeApp.Data;

namespace RecipeApp.Models
{
    public class RecipeSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeToCook { get; set; }
        public int NumberOfIngredients { get; set; }
        public static RecipeSummary FromRecipe(Recipe recipe)
        {
            return new RecipeSummary
            {
                Id = recipe.RecipeId,
                Name = recipe.Name,
                TimeToCook = $"{recipe.TimeToCook.Hours}hrs {recipe.TimeToCook.Minutes}mins"
            };
        }
    }
}
