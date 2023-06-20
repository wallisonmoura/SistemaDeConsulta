using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Models.Entities;
using SistemaDeConsulta.ViewModels.Exame;

namespace SistemaDeConsulta.Controllers
{
    public class ExameController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IValidator<CreateExameViewModel> _createExameValidator;

        public ExameController(ApplicationDBContext dbContext, IValidator<CreateExameViewModel> createExameValidator)
        {
            _dbContext = dbContext;
            _createExameValidator = createExameValidator;
        }

        public IActionResult Index()
        {
            var exameList = _dbContext.Exames.Include(e => e.TipoExame).ToList();

            var viewModelList = exameList.Select(e => new ListExameViewModel
            {
                Id = e.Id,
                Nome = e.Nome,
                Observacoes = e.Observacoes,
                TipoExame = e.TipoExame.Nome,
            }).ToList();

            return View(viewModelList);
        }

        public IActionResult Create()
        {
            var tiposExame = _dbContext.TipoExames.ToList();

            var tiposExameSelectList = tiposExame.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Nome
            }).ToList();

            ViewBag.TiposExame = tiposExameSelectList;
            return View();
        }

        [HttpPost]
        public IActionResult Create (CreateExameViewModel dados)
        {
            try
            {
                var validacao = _createExameValidator.Validate(dados);

                if (!validacao.IsValid)
                {
                    validacao.AddToModelState(ModelState, string.Empty);
                    return View(dados);
                };

                var exame = new Exame
                {
                    Nome = dados.Nome,
                    Observacoes = dados.Observacoes,
                    TipoExameId = dados.TipoExameId
                };

                _dbContext.Exames.Add(exame);
                TempData["MensagemSucesso"] = "Exame cadastrado com sucesso!";
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu paciente, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit() 
        { 
            return View();
        }

        public IActionResult DeleteConfirm()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
