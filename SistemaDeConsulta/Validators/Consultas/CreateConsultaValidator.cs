using FluentValidation;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.ViewModels.Consultas;

namespace SistemaDeConsulta.Validators.Consultas
{
    public class CreateConsultaValidator: AbstractValidator<CreateConsultaViewModel>
    {
        public CreateConsultaValidator(ApplicationDBContext context)
        {
            RuleFor(x => x.PacienteId).NotEmpty().WithMessage("Campo obrigatório.");

            RuleFor(x => x.ExameId).NotEmpty().WithMessage("Campo obrigatório.");

            RuleFor(x => x.DataHora).NotEmpty().WithMessage("Campo obrigatório.")
                .Must(dataHora => !context.Consultas.Any(c => c.DataHora == dataHora))
                .WithMessage("Conflito de horários. Escolha uma data e hora diferente.");
        }
    }
}
