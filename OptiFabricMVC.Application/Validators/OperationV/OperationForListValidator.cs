using FluentValidation;
using OptiFabricMVC.Application.ViewModels.OperationVM;

namespace OptiFabricMVC.Application.Validators.OperationV;

public class OperationForListValidator : AbstractValidator<OperationForListVM>
{
    public OperationForListValidator()
    {
        RuleFor(x => x.CompletedQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Wartość pola musi być równa lub większa niż '0'.");

        RuleFor(x => x.MissingQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Wartość pola musi być równa lub większa niż '0'.");
        
        RuleFor(x => x.RequiredQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Wartość pola musi być równa lub większa niż '0'.");
        
        RuleFor(x=>x.EstimatedTimePerUnit)
            .GreaterThan(TimeSpan.Zero).WithMessage("Przewidywany czas na sztuke musi być większy niż 0");
    }
}