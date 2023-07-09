namespace ReichhartLogistik.Models.EntityModels
{
    public class RecipeIngredientsModel : BaseEntityModel
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public RecipeModel Recipe { get; set; } = null!;
        public IngredientModel Ingredient { get; set; } = null!;
    }
}
