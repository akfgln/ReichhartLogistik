using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;
using FluentAssertions;
using ReichhartLogistik.Data;
using ReichhartLogistik.Data.Entities;
using ReichhartLogistik.Service;

namespace ReichhartLogistik.Test
{
    [TestFixture]
    public class RecipeServicesUnitTest
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRecipeService _recipeService;
        public RecipeServicesUnitTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().
                UseInMemoryDatabase(Guid.NewGuid().ToString());
            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
            _recipeRepository = new EntityRepository<Recipe>(dbContext);

            _recipeService = new RecipeService(_recipeRepository);

        }

        [Test]
        public void TestDeleteRecipe()
        {
            var tRecipe = new Recipe { Name = "Test" };
            _recipeService.InsertRecipeAsync(tRecipe).Wait();
            _recipeService.DeleteRecipeAsync(tRecipe).Wait();
            var recipe = _recipeService.GetRecipeByIdAsync(tRecipe.Id).Result;
            recipe.Deleted.Should().BeTrue();
        }

        [Test]
        public void TestInsertRecipe()
        {
            var tRecipe = new Recipe { Name = "Test" };
            _recipeService.InsertRecipeAsync(tRecipe).Wait();
            var recipe = _recipeService.GetRecipeByIdAsync(tRecipe.Id).Result;
            recipe.Should().NotBeNull();
        }


        [Test]
        public void TestGetRecipes()
        {
            TestInsertRecipe();
            var recipes = _recipeService.GetRecipesAsync().Result;
            recipes.Should().NotBeNull().And.HaveCountGreaterThanOrEqualTo(1);
        }


        [Test]
        public void TestUpdateRecipe()
        {
            var tRecipe = new Recipe { Name = "Test" };
            _recipeService.InsertRecipeAsync(tRecipe).Wait();
            var recipe = _recipeService.GetRecipeByIdAsync(tRecipe.Id).Result;
            recipe.Name="Test2";
            _recipeService.UpdateRecipeAsync(recipe).Wait();
            recipe.Name.Should().BeSameAs("Test2");
        }
    }
}