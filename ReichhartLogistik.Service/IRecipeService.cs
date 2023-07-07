using ReichhartLogistik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Service
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(Recipe recipe);
    }
}
