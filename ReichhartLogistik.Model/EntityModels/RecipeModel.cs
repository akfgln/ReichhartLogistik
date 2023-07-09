using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReichhartLogistik.Models.EntityModels
{
    public class RecipeModel : BaseEntityModel
    {
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public List<RecipeIngredientsModel> RecipeIngredients { get; } = new();
        public List<int> SelectedIngredientIds { get; } = new();
        public List<SelectListItem> AvailableIngredients { get; } = new();
    }
}
