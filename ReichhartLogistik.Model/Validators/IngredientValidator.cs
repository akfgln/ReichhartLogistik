using FluentValidation;
using ReichhartLogistik.Models.EntityModels;

namespace ReichhartLogistik.Models.Validators
{
    public class IngredientValidator : AbstractValidator<IngredientModel>
    {
        public IngredientValidator()
        {
            RuleFor(i => i.Name)
              .NotEmpty().WithMessage("{PropertyName} should be not empty!")
             .Must(i => i.Length >= 2).WithMessage("The length of the {PropertyName} must be at least 2!");
        }
    }
}
