using FluentValidation;
using ReichhartLogistik.Models.EntityModels;

namespace ReichhartLogistik.Models.Validators
{
    public class RecipeValidator : AbstractValidator<RecipeModel>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} sollte nicht leer sein!")
             .Must(r => r.Length >= 2).WithMessage("Die Länge von {PropertyName} muss mindestens 2 betragen!");
            RuleFor(r=>r.SelectedIngredientIds)
             .Must(r => r.Count >= 1).When(x => x.Id == 0).WithMessage("Die Länge von Zutaten muss mindestens 1 betragen!");
        }
    }
}
