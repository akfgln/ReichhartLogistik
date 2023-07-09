using FluentValidation;
using ReichhartLogistik.Models.EntityModels;

namespace ReichhartLogistik.Models.Validators
{
    public class RecipeValidator : AbstractValidator<RecipeModel>
    {
        public RecipeValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("{PropertyName} should be not empty!")
             .Must(r => r.Length >= 2).WithMessage("The length of the {PropertyName} must be at least 2!");
        }
    }
}
