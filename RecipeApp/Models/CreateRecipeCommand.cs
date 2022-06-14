using RecipeApp.Data;

namespace RecipeApp.Models
{
    public class CreateRecipeCommand : EditRecipeBase
    {
        public IList<CreateIngredientCommand> Ingredients { get; set; } = new List<CreateIngredientCommand>();
        public Recipe ToRecipe(ApplicationUser createdBy)
        {
            return new Recipe
            {
                Name = Name,
                TimeToCook = new TimeSpan(TimeToCookHours, TimeToCookMinutes, 0),
                Method = Method,
                IsVegetarian = IsVegetarian,
                IsVegan = IsVegan,
                ingredients = Ingredients?.Select(x => x.ToIngredient()).ToList(),
                CreatedById=createdBy.Id,
            };
        }
    }
}
