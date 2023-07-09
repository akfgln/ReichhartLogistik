using Microsoft.EntityFrameworkCore;
using ReichhartLogistik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Service
{
    public class RecipeIngredientsService : IRecipeIngredientsService
    {
        private readonly IRepository<RecipeIngredients> _recipeIngredientsRepository;
        public RecipeIngredientsService(IRepository<RecipeIngredients> recipeIngredientsRepository)
        {
            _recipeIngredientsRepository = recipeIngredientsRepository;
        }

        public async Task<bool> CheckIfIngredientInRecipeByIngredientId(int ingredientId)
        {
            return await _recipeIngredientsRepository.Table.AnyAsync(x => x.IngredientId == ingredientId);
        }

        public async Task<bool> CheckIfRecipeHasIngredientById(int recipeId)
        {
            return await _recipeIngredientsRepository.Table.AnyAsync(x => x.RecipeId == recipeId);
        }

        public async Task<IEnumerable<RecipeIngredients>> GetRecipeIngredientsByIngredientIdAsync(int ingredientId)
        {
            return await _recipeIngredientsRepository.Table.Where(x => x.IngredientId == ingredientId).ToListAsync();
        }

        public async Task<IEnumerable<RecipeIngredients>> GetRecipeIngredientsByRecipeIdAsync(int recipeId)
        {
            return await _recipeIngredientsRepository.Table.Where(x => x.RecipeId == recipeId).ToListAsync();
        }
    }
}
