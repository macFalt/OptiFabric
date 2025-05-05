using FluentValidation;

namespace OptiFabricMVC.Application.ViewModels.JobVM;

public class BaseJobValidator<T> : AbstractValidator<T> where T : BaseJobVM
{
    public BaseJobValidator()
    {
        RuleFor(x => x.TotalCompletedQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Wartości muszą być dodatnie lub równe 0");
        RuleFor(x => x.TotalMissingQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Wartości muszą być dodatnie lub równe 0");
        RuleFor(x => x.RequiredQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Wartości muszą być dodatnie lub równe 0");
        RuleFor(x=>x.Description)
            .NotEmpty().WithMessage("Pole 'Nazwa' jest wymagane");
    }
}