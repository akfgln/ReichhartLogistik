using ReichhartLogistik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _recipeRepository;
        public RecipeService(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        public async Task DeleteRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.DeleteAsync(recipe);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _recipeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync(bool includeDeleted = true)
        {
            return await _recipeRepository.GetAllAsync(includeDeleted);
        }

        public async Task InsertRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.InsertAsync(recipe);
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.UpdateAsync(recipe);
        }
    }
}
