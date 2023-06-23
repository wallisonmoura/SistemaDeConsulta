using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Models.Entities;
using SistemaDeConsulta.ViewModels.Consultas;

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

            ViewBag.CadastrarPaciente = pacientes.Count == 0;

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

                // Em caso de não haver paciente, ter um redirecionamento para cadastrar paciente
                var pacienteExistente = _dbContext.Pacientes.Any(p => p.Id == dados.PacienteId);
                if (!pacienteExistente)
                {
                    ViewBag.CadastrarPaciente = true;
                    return RedirectToAction("Create", "Paciente");
                }

                ViewBag.CadastrarPaciente = false;

                // Verificar conflito de horários com intervalo de 30 minutos
                var dataHoraConsulta = dados.DataHora ?? DateTime.MinValue;
                var dataHoraFimConsulta = dataHoraConsulta.AddMinutes(30);

                var conflito = _dbContext.Consultas.Any(c =>
                    (c.DataHora >= dataHoraConsulta && c.DataHora < dataHoraFimConsulta) ||
                    (dataHoraConsulta >= c.DataHora && dataHoraConsulta < c.DataHora.AddMinutes(30)));

                if (conflito)
                {
                    TempData["MensagemErro"] = "Já existe uma consulta agendada para o horário informado.";
                    return RedirectToAction("Index");
                }

                var numeroProtocolo = GerarNumeroProtocolo();

                var consulta = new Consulta
                {
                    PacientId = dados.PacienteId,
                    ExameId = dados.ExameId,
                    DataHora = dados.DataHora ?? DateTime.MinValue,
                    NumeroProtocolo = numeroProtocolo
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

        // Para gerar número de protocolo sequencial automático
        private string GerarNumeroProtocolo()
        {
            var ultimoProtocolo = _dbContext.Consultas.OrderByDescending(c => c.Id).FirstOrDefault()?.NumeroProtocolo;
            int ultimoNumero = 0;

            if (!string.IsNullOrEmpty(ultimoProtocolo) && int.TryParse(ultimoProtocolo, out int parsedNumero))
            {
                ultimoNumero = parsedNumero;
            }

            int novoNumero = ultimoNumero + 1;
            string novoProtocolo = novoNumero.ToString().PadLeft(4, '0');
            return novoProtocolo;
        }
    }
}
