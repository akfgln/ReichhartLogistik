using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Data.Entities
{
    public class RecipeIngredients : BaseEntity
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public Ingredient Ingredient { get; set; } = null!;
    }
}
