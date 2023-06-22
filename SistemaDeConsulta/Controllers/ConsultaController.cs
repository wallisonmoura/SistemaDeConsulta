using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Models.Entities;
using SistemaDeConsulta.ViewModels.Consultas;
using SistemaDeConsulta.ViewModels.Pacientes;

namespace SistemaDeConsulta.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IValidator<CreateConsultaViewModel> _createConsultaValidator;

        public ConsultaController(ApplicationDBContext dbContext, IValidator<CreateConsultaViewModel> createConsultaValidator)
        {
            _dbContext = dbContext;
            _createConsultaValidator = createConsultaValidator;
        }

        public IActionResult Index()
        {
            var consultas = _dbContext.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Exame)
                .ToList();

            var viewModelList = consultas.Select(c => new ListConsultaViewModel
            {
                Id = c.Id,
                PacienteNome = c.Paciente.Nome,
                ExameNome = c.Exame.Nome,
                DataHora = c.DataHora,
                NumeroProtocolo = c.NumeroProtocolo
            }).ToList();

            return View(viewModelList);
        }

        public IActionResult Create()
        {
            var pacientes = _dbContext.Pacientes.ToList();
            var exames = _dbContext.Exames.ToList();

            var pacienteItems = pacientes.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Nome
            }).ToList();

            var exameItems = exames.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Nome
            }).ToList();

            ViewBag.Paciente = pacienteItems;
            ViewBag.Exame = exameItems;

            return View();
            
        }

        [HttpPost]
        public IActionResult Create(CreateConsultaViewModel dados)
        {
            try
            {
                var validacao = _createConsultaValidator.Validate(dados);

                if (!validacao.IsValid)
                {
                    validacao.AddToModelState(ModelState, string.Empty);

                    var pacientes = _dbContext.Pacientes.ToList();
                    var exames = _dbContext.Exames.ToList();

                    var pacienteItems = pacientes.Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Nome
                    }).ToList();

                    var exameItems = exames.Select(e => new SelectListItem
                    {
                        Value = e.Id.ToString(),
                        Text = e.Nome
                    }).ToList();

                    ViewBag.Paciente = pacienteItems;
                    ViewBag.Exame = exameItems;

                    return View(dados);
                }

                var consulta = new Consulta
                {
                    PacientId = dados.PacienteId,
                    ExameId = dados.ExameId,
                    DataHora = dados.DataHora as DateTime? ?? DateTime.MinValue,
                    NumeroProtocolo = dados.NumeroProtocolo
                };

                _dbContext.Consultas.Add(consulta);
                TempData["MensagemSucesso"] = "Consulta cadastrado com sucesso!";
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar sua consulta, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
