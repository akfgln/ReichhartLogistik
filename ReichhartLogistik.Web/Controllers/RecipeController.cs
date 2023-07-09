using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using ReichhartLogistik.Data.Entities;
using ReichhartLogistik.Models;
using ReichhartLogistik.Models.EntityModels;
using ReichhartLogistik.Models.Extensions;
using ReichhartLogistik.Service;
using ReichhartLogistik.Web.Services;
using System.Diagnostics;

namespace ReichhartLogistik.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly ILogger<RecipeController> _logger;
        private readonly INotificationService _notificationService;
        private readonly IRecipeService _recipeService;
        private readonly IRecipeIngredientsService _recipeIngredientsService;
        public RecipeController(
            IIngredientService ingredientService,
            ILogger<RecipeController> logger,
            INotificationService notificationService,
            IRecipeService recipeService,
            IRecipeIngredientsService recipeIngredientsService)
        {
            _ingredientService = ingredientService;
            _logger = logger;
            _notificationService = notificationService;
            _recipeService = recipeService;
            _recipeIngredientsService = recipeIngredientsService;
        }

        public async Task<IActionResult> Index(bool? includeDeleted)
        {
            var recipeList = await _recipeService.GetRecipesAsync(includeDeleted: includeDeleted.GetValueOrDefault(false));
            var model = recipeList.ToModelList<IEnumerable<RecipeModel>>();
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = new RecipeModel();
            await PrepareAvaliableIngredientsAsync(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeModel recipeModel)
        {
            if (ModelState.IsValid)
            {
                var recipe = recipeModel.ToEntity<Recipe>();
                await _recipeService.InsertRecipeAsync(recipe);
                recipe.RecipeIngredients.AddRange(recipeModel.SelectedIngredientIds.Select(x => new RecipeIngredients { IngredientId = x, RecipeId = recipe.Id }));
                await _recipeService.UpdateRecipeAsync(recipe);
                _notificationService.SuccessNotification("Neues Rezept wurde erstellt!");
                return RedirectToAction("Edit", recipe.Id);
            }

            _notificationService.ErrorNotification("ein Fehler ist aufgetreten!");
            return View(recipeModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id > 0)
            {
                var model = new RecipeModel();
                var recipe = await _recipeService.GetRecipeByIdAsync(id);
                if (recipe != null)
                {
                    model = recipe.ToModel<RecipeModel>();
                    await PrepareRecipeIngredients(model, recipe, true);
                    return View(model);
                }
            }
            _notificationService.ErrorNotification("Das Rezept ist nicht gefunden!");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var model = new RecipeModel();
                var recipe = await _recipeService.GetRecipeByIdAsync(id);
                if (recipe != null)
                {
                    model = recipe.ToModel<RecipeModel>();
                    await PrepareRecipeIngredients(model, recipe, true);
                    await PrepareAvaliableIngredientsAsync(model);
                    return View(model);
                }
            }
            _notificationService.ErrorNotification("Das Rezept ist nicht gefunden!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RecipeModel recipeModel)
        {
            if (ModelState.IsValid)
            {
                var recipe = await _recipeService.GetRecipeByIdAsync(recipeModel.Id);
                if (recipe != null)
                {
                    recipe.Name = recipeModel.Name;
                    recipe.Deleted = recipeModel.Deleted;
                    await _recipeService.UpdateRecipeAsync(recipe);
                    _notificationService.SuccessNotification("Das Rezept wurde aktualisiert!");
                }
                else
                {
                    _notificationService.ErrorNotification("Das Rezept ist nicht gefunden!");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                _notificationService.ErrorNotification("Das Modell ist ungültig!");
            }

            return View(recipeModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected async Task PrepareRecipeIngredients(RecipeModel model, Recipe recipe, bool includeDetails = false)
        {
            var recipeIngredients = await _recipeIngredientsService.GetRecipeIngredientsByRecipeIdAsync(recipe.Id);
            if (includeDetails)
            {
                var ingredientIds = recipe.RecipeIngredients.Select(x => x.IngredientId).ToList();
                var ingredients = await _ingredientService.GetIngredientsByIdsAsync(ingredientIds);
                foreach (var item in recipe.RecipeIngredients)
                {
                    item.Ingredient = ingredients.FirstOrDefault(x => x.Id == item.IngredientId);
                }
            }
            model.RecipeIngredients.AddRange(recipeIngredients.ToModelList<List<RecipeIngredientsModel>>());
            model.SelectedIngredientIds.AddRange(model.RecipeIngredients.Select(x => x.IngredientId).ToList());
        }

        protected async Task PrepareAvaliableIngredientsAsync(RecipeModel model)
        {
            if (model.AvailableIngredients == null)
                throw new ArgumentNullException(nameof(model.AvailableIngredients));

            var ingredients = await _ingredientService.GetIngredientsAsync();
            if (ingredients != null && ingredients.Any())
            {
                foreach (var ingredient in ingredients)
                {
                    model.AvailableIngredients.Add(new SelectListItem { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                }
            }
        }
    }
}