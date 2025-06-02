using FluentValidation;
using OptiFabricMVC.Application.ViewModels.OperationVM;

namespace OptiFabricMVC.Application.Validators.OperationV;

public class OperationPatternForListValidator : AbstractValidator<OperationPatternForListVM>
{
    public OperationPatternForListValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Podanie nazwy operacji jest wymagane");
        RuleFor(x => x.EstimatedTimePerUnit)
            .GreaterThan(TimeSpan.Zero).WithMessage("Przewidywany czas na sztuke musi być większy niż 0");


    }
}