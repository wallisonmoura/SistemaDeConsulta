using FluentValidation;
using SistemaDeConsulta.ViewModels.Exames;

namespace SistemaDeConsulta.Validators.Exames
{
    public class EditExameValidator: AbstractValidator<EditExameViewModel>
    {
        public EditExameValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório.")
                 .MaximumLength(100).WithMessage("O nome deve ter até 100 caracteres.");

           RuleFor(x => x.Observacoes).NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(1000).WithMessage("A observação deve ter até 100 caracteres.");

           RuleFor(x => x.TipoExameId).NotEmpty().WithMessage("Campo obrigatório.");
        }
    }
}
