using System.ComponentModel.DataAnnotations;

namespace RecipeApp.Models
{
    public class EditRecipeBase
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Range(0, 24), Display(Name = "Time to cook (hrs)")]
        public int TimeToCookHours { get; set; }
        [Range(0, 24), Display(Name = "Time to cook (mins)")]
        public int TimeToCookMinutes { get; set; }
        public string Method { get; set; }
        [Display(Name = "Vegetarian?")]
        public bool IsVegetarian { get; set; }
        [Display(Name = "Vegan?")]
        public bool IsVegan { get; set; }

    }
}
