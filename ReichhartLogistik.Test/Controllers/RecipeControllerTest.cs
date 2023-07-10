using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ReichhartLogistik.Service;
using ReichhartLogistik.Web.Controllers;
using ReichhartLogistik.Web.Services;
using Serilog;

namespace ReichhartLogistik.Test.Controllers
{
    [TestFixture]
    public class RecipeControllerTest
    {

        private readonly Mock<IIngredientService> _ingredientService;
        private readonly Mock<ILogger<RecipeController>> _logger;
        private readonly Mock<INotificationService> _notificationService;
        private readonly Mock<IRecipeService> _recipeService;
        private readonly Mock<IRecipeIngredientsService> _recipeIngredientsService;
        private readonly RecipeController _recipeController;
        public RecipeControllerTest()
        {
            ReichhartLogistik.Models.AutoMapper.InitializeAutomapper();
            _ingredientService = new Mock<IIngredientService>();
            _logger = new Mock<ILogger<RecipeController>>();
            _notificationService = new Mock<INotificationService>();
            _recipeService = new Mock<IRecipeService>();
            _recipeIngredientsService = new Mock<IRecipeIngredientsService>();
            _recipeController = new RecipeController(_ingredientService.Object, _logger.Object, _notificationService.Object, _recipeService.Object, _recipeIngredientsService.Object);
        }

        [Test]
        public void IndexWithIncludeDeleted()
        {
            var result = _recipeController.Index(true).Result;
            result.Should().BeOfType(typeof(ViewResult));
        }

    }
}
