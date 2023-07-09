using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Data.Entities
{
    public class Ingredient : BaseEntity
    {
        public required string Name { get; set; }
        public List<RecipeIngredients> RecipeIngredients { get; } = new();
    }
}
