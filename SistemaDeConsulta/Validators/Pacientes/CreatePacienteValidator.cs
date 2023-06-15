using FluentValidation;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.ViewModels.Pacientes;
using System.Text.RegularExpressions;

namespace SistemaDeConsulta.Validators.Pacientes
{
    public class CreatePacienteValidator: AbstractValidator<CreatePacienteViewModel>
    {
        public CreatePacienteValidator(ApplicationDBContext context) 
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Campo obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter até {MaxLength} caracteres.");

            RuleFor(x => x.CPF).NotEmpty().WithMessage("Campo obrigatório.")
                .Must(cpf => Regex.Replace(cpf, "[^0-9]", "").Length == 11).WithMessage("O CPF deve ter até {MaxLength} caracteres.")
                .Must(cpf => !context.Pacientes.Any(p => p.CPF == cpf)).WithMessage("Este CPF já está em uso.");

            RuleFor(x => x.DataNascimento).NotEmpty().WithMessage("Campo obrigatório.")
                .Must(data => data <= DateTime.Today).WithMessage("A data de nascimento não pode ser futura.");

            RuleFor(x => x.Sexo).NotNull().WithMessage("Campo obrigatório.");

            RuleFor(x => x.Telefone).NotEmpty().WithMessage("Campo obrigatório.")
                .Matches(@"^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$")
                .WithMessage("Formato de telefone inválido. Utilize o formato (99) 99999-9999.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Campo obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.");


        }
    }
}
