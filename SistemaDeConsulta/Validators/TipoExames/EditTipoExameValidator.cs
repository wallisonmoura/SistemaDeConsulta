using FluentValidation;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.ViewModels.TipoExames;

namespace SistemaDeConsulta.Validators.TipoExames
{
    public class EditTipoExameValidator: AbstractValidator<EditTipoExamesViewModel>
    {
        public EditTipoExameValidator(ApplicationDBContext context) 
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório.")
                 .MaximumLength(100).WithMessage("O nome deve ter até 100 caracteres.")
                 .Must(nome => !context.TipoExames.Any(p => p.Nome == nome)).WithMessage("Tipo de exame já cadastrado.");

            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(256).WithMessage("A descrição deve ter até 256 caracteres.");
        }
    }
}
