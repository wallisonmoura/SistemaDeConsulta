﻿using SistemaDeConsulta.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeConsulta.ViewModels.Pacientes
{
    public class CreatePacienteViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até {1} caracteres.")]
        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        public SexoEnum Sexo { get; set; }

        [RegularExpression(@"^\([0-9]{2}\) [0-9]{4,5}-[0-9]{4}$", ErrorMessage = "Formato de telefone inválido. Utilize o formato (99) 99999-9999.")]
        public string Telefone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
