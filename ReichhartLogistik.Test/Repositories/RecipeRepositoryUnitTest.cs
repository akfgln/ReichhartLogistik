using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReichhartLogistik.Data.Entities;
using ReichhartLogistik.Data;
using FluentAssertions;

namespace ReichhartLogistik.Test.Repositories
{
    [TestFixture]
    public class RecipeRepositoryUnitTest
    {
        private readonly IRepository<Recipe> _recipeRepository;
        public RecipeRepositoryUnitTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            _recipeRepository = new EntityRepository<Recipe>(dbContext);
        }

        [Test]
        public async Task TestInsertRecipe()
        {
            var tRecipe = new Recipe { Name = "Test" };
            await _recipeRepository.InsertAsync(tRecipe);
            var recipe = await _recipeRepository.GetByIdAsync(tRecipe.Id);
            recipe.Should().NotBeNull();
        }

        [Test]
        public async Task TestDeleteRecipe()
        {
            var tRecipe = new Recipe { Name = "Test" };
            await _recipeRepository.InsertAsync(tRecipe);
            var recipe = await _recipeRepository.GetByIdAsync(tRecipe.Id);
            _recipeRepository.DeleteAsync(recipe).Wait();
            recipe = await _recipeRepository.GetByIdAsync(tRecipe.Id);
            recipe.Deleted.Should().BeTrue();
        }
        /*
           Task<T> GetByIdAsync(int id, bool includeDeleted = true);

        Task<IList<T>> GetByIdsAsync(IList<int> ids, bool includeDeleted = true);

        Task<IList<T>> GetAllAsync(bool includeDeleted = true);

        Task<IPagedList<T>> GetAllPagedAsync(int pageIndex = 0, int pageSize = int.MaxValue, bool getOnlyTotalCount = false, bool includeDeleted = true);

        Task InsertAsync(T entity, bool pushEvent = true);

        Task InsertAsync(IList<T> entities, bool pushEvent = true);

        Task UpdateAsync(T entity, bool pushEvent = true);

        Task UpdateAsync(IList<T> entities, bool pushEvent = true);

        Task DeleteAsync(T entity, bool pushEvent = true);

        Task DeleteAsync(IList<T> entities, bool pushEvent = true);
        +*/
    }
}
