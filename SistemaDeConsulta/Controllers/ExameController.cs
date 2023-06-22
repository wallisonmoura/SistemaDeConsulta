using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Models.Entities;
using SistemaDeConsulta.ViewModels.Exames;

namespace SistemaDeConsulta.Controllers
{
    public class ExameController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IValidator<CreateExameViewModel> _createExameValidator;
        private readonly IValidator<EditExameViewModel> _editExameValidator;

        public ExameController(ApplicationDBContext dbContext, IValidator<CreateExameViewModel> createExameValidator, IValidator<EditExameViewModel> editExameValidator)
        {
            _dbContext = dbContext;
            _createExameValidator = createExameValidator;
            _editExameValidator = editExameValidator;
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

                    var tiposExame = _dbContext.TipoExames.ToList();

                    var tiposExameSelectList = tiposExame.Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Nome
                    }).ToList();

                    ViewBag.TiposExame = tiposExameSelectList;

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
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu exame, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id) 
        {
            var exame = _dbContext.Exames.Find(id);
            
            if (exame != null)
            {
                var tiposExame = _dbContext.TipoExames.ToList();

                var tiposExameSelectList = tiposExame.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Nome
                }).ToList();

                ViewBag.TiposExame = tiposExameSelectList;

                return View(new EditExameViewModel
                {
                    Id = exame.Id,
                    Nome = exame.Nome,
                    Observacoes = exame.Observacoes,
                    TipoExameId = exame.TipoExameId
                });
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(int id, EditExameViewModel dados)
        {
            try
            {
                var validacao = _editExameValidator.Validate(dados);
                if (!validacao.IsValid)
                {
                    var tiposExame = _dbContext.TipoExames.ToList();

                    var tiposExameSelectList = tiposExame.Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Nome
                    }).ToList();

                    ViewBag.TiposExame = tiposExameSelectList;

                    validacao.AddToModelState(ModelState, string.Empty);
                    return View(dados);

                }

                var exame = _dbContext.Exames.Find(id);

                if (exame != null)
                {
                    exame.Id = dados.Id;
                    exame.Nome = dados.Nome;
                    exame.Observacoes = dados.Observacoes;
                    exame.TipoExameId = dados.TipoExameId;

                    _dbContext.Exames.Update(exame);
                    TempData["MensagemSucesso"] = "Exame alterado com sucesso!";
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu exame, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }

        public IActionResult DeleteConfirm(int id)
        {
            var exame = _dbContext.Exames.Find(id);
            return View(exame);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var exame = _dbContext.Exames.Find(id);

                if (exame != null)
                {
                    _dbContext.Exames.Remove(exame);
                    TempData["MensagemSucesso"] = "Exame excluído com sucesso!";
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }

                return NotFound();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu exame, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
