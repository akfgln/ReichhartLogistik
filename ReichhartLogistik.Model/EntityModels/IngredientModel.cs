using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Models.EntityModels
{
    public class IngredientModel : BaseEntityModel
    {
        public string Name { get; set; }
        public List<RecipeIngredientsModel> RecipeIngredients { get; } = new();
    }
}
