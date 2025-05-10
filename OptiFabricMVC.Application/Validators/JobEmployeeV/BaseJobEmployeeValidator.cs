using FluentValidation;
using OptiFabricMVC.Application.ViewModels.JobEmployeeVM;

namespace OptiFabricMVC.Application.Validators.JobEmployeeV;

public class BaseJobEmployeeValidator<T> : AbstractValidator<T> where T : BaseJobEmployeeVM
{
    public BaseJobEmployeeValidator()
    {
        RuleFor(x => x.CompletedQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Wartość pola musi być równa lub większa niż '0'.");

        RuleFor(x => x.MissingQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Wartość pola musi być równa lub większa niż '0'.");
    }
}