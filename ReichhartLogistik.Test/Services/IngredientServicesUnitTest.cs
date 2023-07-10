using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReichhartLogistik.Data.Entities;
using ReichhartLogistik.Data;
using ReichhartLogistik.Service;
using FluentAssertions;

namespace ReichhartLogistik.Test.Services
{
    [TestFixture]
    public class IngredientServicesUnitTest
    {
        private readonly IIngredientService _ingredientService;
        public IngredientServicesUnitTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            IRepository<Ingredient> _ingredientRepository = new EntityRepository<Ingredient>(dbContext);
            _ingredientService = new IngredientService(_ingredientRepository);
        }

        [Test]
        public async Task TestDeleteIngredientThatHasNoRecipe()
        {
            Ingredient tIngredient = await InsertIngredient();
            await _ingredientService.DeleteIngredientAsync(tIngredient);
            var ingredient = await _ingredientService.GetIngredientByIdAsync(tIngredient.Id);
            ingredient.Should().BeNull();
        }

        private async Task<Ingredient> InsertIngredient()
        {
            var tIngredient = new Ingredient { Name = Guid.NewGuid().ToString() };
            await _ingredientService.InsertIngredientAsync(tIngredient);
            return tIngredient;
        }
        /*
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<IEnumerable<Ingredient>> GetIngredientsByIdsAsync(List<int> ids);
        Task InsertIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(Ingredient ingredient);*/
    }
}