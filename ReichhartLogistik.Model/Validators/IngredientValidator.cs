using FluentValidation;
using ReichhartLogistik.Models.EntityModels;

namespace ReichhartLogistik.Models.Validators
{
    public class IngredientValidator : AbstractValidator<IngredientModel>
    {
        public IngredientValidator()
        {
            RuleFor(i => i.Name)
              .NotEmpty().WithMessage("{PropertyName} sollte nicht leer sein!")
             .Must(i => i.Length >= 2).WithMessage("Die Länge von {PropertyName} muss mindestens 2 betragen!");
        }
    }
}
