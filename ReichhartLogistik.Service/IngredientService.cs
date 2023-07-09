using ReichhartLogistik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Service
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Ingredient> _ingredientRepository;
        public IngredientService(IRepository<Ingredient> ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public async Task DeleteIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.DeleteAsync(ingredient);
        }

        public async Task InsertIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.InsertAsync(ingredient);
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _ingredientRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _ingredientRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByIdsAsync(List<int> ids)
        {
            return await _ingredientRepository.GetByIdsAsync(ids);
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await _ingredientRepository.UpdateAsync(ingredient);
        }
    }
}
