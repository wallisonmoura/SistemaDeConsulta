using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Models.Entities;
using SistemaDeConsulta.Models.Enums;
using SistemaDeConsulta.ViewModels.Pacientes;
using System.Text.RegularExpressions;

namespace SistemaDeConsulta.Controllers
{
    public class PacienteController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IValidator<CreatePacienteViewModel> _createPacienteValidator;
        //private readonly IValidator<EditPacienteViewModel> _editPacienteValidator;

        public PacienteController(ApplicationDBContext dbContext, IValidator<CreatePacienteViewModel> createPacienteValidator, IValidator<EditPacienteViewModel> editPacienteValidator)
        {
            _dbContext = dbContext;
            _createPacienteValidator = createPacienteValidator;
            //_editPacienteValidator = editPacienteValidator;

        }
        public IActionResult Index()
        {
            return View();
        }

        // POST: Paciente/Create
        public IActionResult Create()
        {
            ViewBag.Sexo = new[]
            {
                new SelectListItem { Text = "Masculino", Value = SexoEnum.Masculino.ToString()},
                new SelectListItem { Text = "Feminino", Value = SexoEnum.Feminino.ToString()},
            };
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePacienteViewModel dados)
        {
            var validacao = _createPacienteValidator?.Validate(dados);

            if (validacao != null && !validacao.IsValid)
            {
                ViewBag.Sexo = new[]
                {
                    new SelectListItem { Text = "Masculino", Value = SexoEnum.Masculino.ToString()},
                    new SelectListItem { Text = "Feminino", Value = SexoEnum.Feminino.ToString()},
                };

                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var paciente = new Paciente
            {
                Nome = dados.Nome,
                CPF = Regex.Replace(dados.CPF, "[^0-9]", ""),
                DataNascimento = dados.DataNascimento,
                Sexo = dados.Sexo,
                Telefone = dados.Telefone,
                Email = dados.Email
            };

            _dbContext.Pacientes.Add(paciente);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit() 
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
