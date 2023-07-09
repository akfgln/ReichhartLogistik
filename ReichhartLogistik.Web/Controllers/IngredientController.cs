using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReichhartLogistik.Data.Entities;
using ReichhartLogistik.Models.EntityModels;
using ReichhartLogistik.Models.Extensions;
using ReichhartLogistik.Service;
using ReichhartLogistik.Web.Services;

namespace ReichhartLogistik.Web.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly IRecipeIngredientsService _recipeIngredientsService;
        private readonly INotificationService _notificationService;
        public IngredientController(
            IIngredientService ingredientService,
            IRecipeIngredientsService recipeIngredientsService,
            INotificationService notificationService)
        {
            _ingredientService = ingredientService;
            _recipeIngredientsService = recipeIngredientsService;
            _notificationService = notificationService;
        }
        // GET: IngredientController
        public async Task<IActionResult> Index()
        {
            var ingredientList = await _ingredientService.GetIngredientsAsync();
            var model = ingredientList.ToModelList<IEnumerable<IngredientModel>>();
            return View(model);
        }

        // GET: IngredientController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: IngredientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IngredientModel ingredientModel)
        {
            if (ModelState.IsValid)
            {
                var ingredient = ingredientModel.ToEntity<Ingredient>();
                await _ingredientService.InsertIngredientAsync(ingredient);
                _notificationService.SuccessNotification("Die Zutat wurde erstellt!");
                //return RedirectToAction("Edit", new { id = ingredient.Id });
                return RedirectToAction("Index");

            }

            _notificationService.ErrorNotification("Das Modell ist ungültig!");

            return View(ingredientModel);
        }


        // GET: IngredientController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id > 0)
            {
                var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
                if (ingredient != null)
                {
                    var model = ingredient.ToModel<IngredientModel>();
                    return View(model);
                }
            }
            _notificationService.ErrorNotification("Die Zutat ist nicht gefunden!");
            return RedirectToAction("Index");
        }

        // POST: IngredientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IngredientModel ingredientModel)
        {
            if (ModelState.IsValid)
            {
                var ingredient = await _ingredientService.GetIngredientByIdAsync(ingredientModel.Id);
                if (ingredient != null)
                {
                    ingredient.Name = ingredientModel.Name;
                    await _ingredientService.UpdateIngredientAsync(ingredient);
                    _notificationService.SuccessNotification("Die Zutat wurde aktualisiert!");
                }
                else
                {
                    _notificationService.ErrorNotification("Die Zutat ist nicht gefunden!");
                }
                return RedirectToAction("Index");

            }
            else
            {
                _notificationService.ErrorNotification("Das Modell ist ungültig!");
            }

            return View(ingredientModel);
        }

        // GET: IngredientController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var model = new IngredientModel();
                var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
                if (ingredient != null)
                {
                    model = ingredient.ToModel<IngredientModel>();
                    return View(model);
                }
            }
            _notificationService.ErrorNotification("Die Zutat ist nicht gefunden!");
            return RedirectToAction("Index");
        }

        // POST: IngredientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            if (id > 0)
            {
                var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
                if (ingredient != null)
                {
                    if (!await _recipeIngredientsService.CheckIfIngredientInRecipeByIngredientId(id))
                    {
                        await _ingredientService.DeleteIngredientAsync(ingredient);
                        _notificationService.SuccessNotification("Die Zutat wurde gelöscht!");
                        return RedirectToAction("Index");
                    }

                    _notificationService.ErrorNotification("Die Zutat kann nicht gelöscht werden, da sie in einem anderen Rezept enthalten ist!");
                    var model = ingredient.ToModel<IngredientModel>();
                    return View(model);
                }
            }
            _notificationService.ErrorNotification("Die Zutat ist nicht gefunden!");
            return RedirectToAction("Index");
        }
    }
}
