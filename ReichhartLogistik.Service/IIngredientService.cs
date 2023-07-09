using ReichhartLogistik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Service
{
    public interface IIngredientService
    {
        Task DeleteIngredientAsync(Ingredient ingredient);
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<IEnumerable<Ingredient>> GetIngredientsByIdsAsync(List<int> ids);
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task InsertIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(Ingredient ingredient);
    }
}
