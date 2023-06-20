using FluentValidation;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.ViewModels.Exames;

namespace SistemaDeConsulta.Validators.Exames
{
    public class CreateExameValidator: AbstractValidator<CreateExameViewModel>
    {
        public CreateExameValidator(ApplicationDBContext context)
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório.")
                 .MaximumLength(100).WithMessage("O nome deve ter até 100 caracteres.")
                 .Must(nome => !context.Exames.Any(p => p.Nome == nome)).WithMessage("Exame já cadastrado.");

           RuleFor(x => x.Observacoes).NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(1000).WithMessage("A observação deve ter até 100 caracteres.");

           RuleFor(x => x.TipoExameId).NotEmpty().WithMessage("Campo obrigatório.");
        }
    }
}
