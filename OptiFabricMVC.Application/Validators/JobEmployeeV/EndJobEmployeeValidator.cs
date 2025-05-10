using FluentValidation;
using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

namespace OptiFabricMVC.Application.Validators.JobEmployeeV;

public class EndJobEmployeeValidator : AbstractValidator<EndJobEmployeeVM>
{
    public EndJobEmployeeValidator()
    {
        RuleFor(x => x.CompletedQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Wartość pola musi być równa lub większa niż '0'.");
    }
}