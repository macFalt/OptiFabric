using FluentValidation;

namespace OptiFabricMVC.Application.ViewModels.ProductsVM;

public class BaseProductValidator<T> : AbstractValidator<T> where T : BaseProductVM
{
    public BaseProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Pole 'Nazwa' jest wymagane");
        RuleFor(x=>x.Material)
            .NotEmpty().WithMessage("Pole 'Material' jest wymagane");
        RuleFor(x=>x.DrawingNumber)
            .NotEmpty().WithMessage("Pole 'Numer rysunku' jest wymagane");
    }
}