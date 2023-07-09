using AutoMapper;
using ReichhartLogistik.Data.Entities;
using ReichhartLogistik.Models.EntityModels;

namespace ReichhartLogistik.Models
{
    public static class AutoMapper
    {
        public static IMapper Mapper { get; private set; }
        public static MapperConfiguration MapperConfiguration { get; private set; }
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Recipe, RecipeModel>();
                cfg.CreateMap<RecipeModel, Recipe>();

                cfg.CreateMap<Ingredient, IngredientModel>();
                cfg.CreateMap<IngredientModel, Ingredient>();

                cfg.CreateMap<RecipeIngredients, RecipeIngredientsModel>();
                cfg.CreateMap<RecipeIngredientsModel, RecipeIngredients>();
            });
            MapperConfiguration = config;
            var mapper = new Mapper(config);
            Mapper = mapper;
            return mapper;
        }
    }
}
