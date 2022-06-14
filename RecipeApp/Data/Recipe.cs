using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Data
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public TimeSpan TimeToCook { get; set; }
        public bool IsDeleted { get; set; }
        public string Method { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public string CreatedById { get; set; }
        public ICollection<Ingredient> ingredients { get; set; }
    }
}
