using FluentValidation.AspNetCore;
using FluentValidation;
using ReichhartLogistik.Models.Validators;
using ReichhartLogistik.Data.Entities;
using ReichhartLogistik.Service;
using ReichhartLogistik.Web.Services;
using System.Reflection;

namespace ReichhartLogistik.Models
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureApplicationServices(this IServiceCollection services,
           WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INotificationService, NotificationService>();

            AutoMapper.InitializeAutomapper();

            var mvcBuilder = services.AddControllersWithViews();

#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif
            //add fluent validation
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

            services.AddValidatorsFromAssemblyContaining<RecipeValidator>();
            //services.AddValidatorsFromAssemblyContaining<RecipeValidator>(ServiceLifetime.Scoped);

            builder.Services.AddScoped<IRepository<Recipe>, EntityRepository<Recipe>>();
            builder.Services.AddScoped<IRecipeService, RecipeService>();

            builder.Services.AddScoped<IRepository<Ingredient>, EntityRepository<Ingredient>>();
            builder.Services.AddScoped<IIngredientService, IngredientService>();

            builder.Services.AddScoped<IRepository<RecipeIngredients>, EntityRepository<RecipeIngredients>>();
            builder.Services.AddScoped<IRecipeIngredientsService, RecipeIngredientsService>();

        }

    }
}
