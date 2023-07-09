using ReichhartLogistik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Service
{
    public interface IRecipeIngredientsService
    {
        Task<bool> CheckIfIngredientInRecipeByIngredientId(int ingredientId);
        Task<bool> CheckIfRecipeHasIngredientById(int recipeId);
        Task<IEnumerable<RecipeIngredients>> GetRecipeIngredientsByRecipeIdAsync(int recipeId);
        Task<IEnumerable<RecipeIngredients>> GetRecipeIngredientsByIngredientIdAsync(int ingredientId);
    }
}
