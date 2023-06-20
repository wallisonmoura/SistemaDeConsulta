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
        private readonly IValidator<EditPacienteViewModel> _editPacienteValidator;

        public PacienteController(ApplicationDBContext dbContext, IValidator<CreatePacienteViewModel> createPacienteValidator, IValidator<EditPacienteViewModel> editPacienteValidator)
        {
            _dbContext = dbContext;
            _createPacienteValidator = createPacienteValidator;
            _editPacienteValidator = editPacienteValidator;

        }


        public IActionResult Index()
        {
            var pacientes = _dbContext.Pacientes.ToList();

            var viewModelList = pacientes.Select(p => new ListTipoExamesViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                CPF = p.CPF,
                DataNascimento = p.DataNascimento,
                Sexo = p.Sexo,
                Telefone = p.Telefone,
                Email = p.Email
            }).ToList();

            return View(viewModelList);
        }

 
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
            try
            {
                var validacao = _createPacienteValidator.Validate(dados);

                if (!validacao.IsValid)
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
                TempData["MensagemSucesso"] = "Paciente cadastrado com sucesso!";
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            } catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu paciente, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id) 
        {
            var paciente = _dbContext.Pacientes.Find(id);

            if (paciente != null)
            {
                ViewBag.Sexo = new[]
                {
                    new SelectListItem { Text = "Masculino", Value = SexoEnum.Masculino.ToString()},
                    new SelectListItem { Text = "Feminino", Value = SexoEnum.Feminino.ToString()},
                };

                return View(new EditPacienteViewModel
                {
                    Id = paciente.Id,
                    Nome = paciente.Nome,
                    CPF = paciente.CPF,
                    DataNascimento = paciente.DataNascimento,
                    Sexo = paciente.Sexo,
                    Telefone = paciente.Telefone,
                    Email = paciente.Email
                });
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(int id, EditPacienteViewModel dados)
        {
            try
            {
                var validacao = _editPacienteValidator.Validate(dados);

                if (!validacao.IsValid)
                {
                    ViewBag.Sexo = new[]
                                    {
                    new SelectListItem { Text = "Masculino", Value = SexoEnum.Masculino.ToString()},
                    new SelectListItem { Text = "Feminino", Value = SexoEnum.Feminino.ToString()},
                };

                    validacao.AddToModelState(ModelState, string.Empty);
                    return View(dados);
                }

                var paciente = _dbContext.Pacientes.Find(id);

                if (paciente != null)
                {
                    paciente.Nome = dados.Nome;
                    paciente.CPF = Regex.Replace(dados.CPF, "[^0-9]", "");
                    paciente.DataNascimento = dados.DataNascimento;
                    paciente.Sexo = dados.Sexo;
                    paciente.Telefone = dados.Telefone;
                    paciente.Email = dados.Email;

                    _dbContext.Pacientes.Update(paciente);
                    TempData["MensagemSucesso"] = "Paciente alterado com sucesso!";
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }

                return NotFound();
            }
            catch (Exception erro) 
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu paciente, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }


        public IActionResult DeleteConfirm(int id)
        {
            var paciente = _dbContext.Pacientes.Find(id);
            return View(paciente);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var paciente = _dbContext.Pacientes.Find(id);

                if (paciente != null)
                {
                    _dbContext.Pacientes.Remove(paciente);
                    TempData["MensagemSucesso"] = "Paciente excluído com sucesso!";
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }

                return NotFound();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu paciente, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
