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
        Task DeleteRecipeAsync(Recipe recipe);
        Task<IEnumerable<Recipe>> GetRecipesAsync(bool includeDeleted = true);
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task InsertRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
    }
}
