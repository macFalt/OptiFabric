using FluentValidation;
using OptiFabricMVC.Application.ViewModels.JobVM;

namespace OptiFabricMVC.Application.Validators.JobV;

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
        RuleFor(x => x)
            .Must(x => x.TotalCompletedQuantity + x.TotalMissingQuantity <= x.RequiredQuantity)
            .WithMessage("Suma dobrych i braków nie może przekraczać wymaganego nakładu.");

    }
}