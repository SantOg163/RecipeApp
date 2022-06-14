using RecipeApp.Data;
using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class CreateIngredientCommand
    {
        [Required, StringLength(50)]
        public string Name { get; set; }    
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
        public Ingredient ToIngredient()
        {
            return new Ingredient { Name=Name, Quantity=Quantity, Unit=Unit};
        }
    }
}
