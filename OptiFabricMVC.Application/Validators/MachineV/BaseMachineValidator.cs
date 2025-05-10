using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using FluentValidation;
using OptiFabricMVC.Application.ViewModels.MachinesVM;

namespace OptiFabricMVC.Application.Validators.MachineV;

public class BaseMachineValidator<T> : AbstractValidator<T> where T : BaseMachineVM
{
    public BaseMachineValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty().WithMessage("Pole 'Name' jest wymagane");
        RuleFor(x=>x.Type)
            .NotEmpty().WithMessage("Pole 'Typ' jest wymagane");
        RuleFor(x=>x.Status)
            .NotEmpty().WithMessage("Pole 'Status' jest wymagane");
    }
}