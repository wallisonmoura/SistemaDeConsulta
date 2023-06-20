using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Models.Entities;
using SistemaDeConsulta.ViewModels.TipoExames;

namespace SistemaDeConsulta.Controllers
{
    public class TipoExameController: Controller
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IValidator<CreateTipoExamesViewModel> _createTipoExameValidator;
        private readonly IValidator<EditTipoExamesViewModel> _editTipoExameValidator;

        public TipoExameController(ApplicationDBContext dbContext, IValidator<CreateTipoExamesViewModel> createTipoExameValidator, IValidator<EditTipoExamesViewModel> editTipoExameValidator)
        {
            _dbContext = dbContext;
            _createTipoExameValidator = createTipoExameValidator;
            _editTipoExameValidator = editTipoExameValidator;
        }
        public IActionResult Index()
        {
            var tipoExame = _dbContext.TipoExames.ToList();

            var viewModelList = tipoExame.Select(p => new ListTipoExamesViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
            }).ToList();


            return View(viewModelList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTipoExamesViewModel dados)
        {
            try
            {
                var validacao = _createTipoExameValidator.Validate(dados);

                if (!validacao.IsValid)
                {
                    validacao.AddToModelState(ModelState, string.Empty);
                    return View(dados);
                }

                var tipoExame = new TipoExame
                {
                    Nome = dados.Nome,
                    Descricao = dados.Descricao
                };

                _dbContext.TipoExames.Add(tipoExame);
                TempData["MensagemSucesso"] = "Tipo exame cadastrado com sucesso!";
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu tipo de exame, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(int id)
        {
            var tipoExame = _dbContext.TipoExames.Find(id);
            if (tipoExame != null)
            {

                return View(new EditTipoExamesViewModel
                {
                    Id = tipoExame.Id,
                    Nome = tipoExame.Nome,
                    Descricao = tipoExame.Descricao
                    
                });
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(int id, EditTipoExamesViewModel dados)
        {
            try
            {
                var validacao = _editTipoExameValidator.Validate(dados);

                if (!validacao.IsValid)
                {
                    validacao.AddToModelState(ModelState, string.Empty);
                    return View(dados);
                }

                var tipoExame = _dbContext.TipoExames.Find(id);

                if (tipoExame != null)
                {
                    tipoExame.Nome = dados.Nome;
                    tipoExame.Descricao = dados.Descricao;

                    _dbContext.TipoExames.Update(tipoExame);
                    TempData["MensagemSucesso"] = "Tipo exame alterado com sucesso!";
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }

                return NotFound();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu tipo de exame, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult DeleteConfirm(int id)
        {
            var tipoExame = _dbContext.TipoExames.Find(id);
            return View(tipoExame);
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var tipoExame = _dbContext.TipoExames.Find(id);

                if (tipoExame != null)
                {
                    _dbContext.TipoExames.Remove(tipoExame);
                    TempData["MensagemSucesso"] = "Tipo exame excluído com sucesso!";
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }

                return NotFound();
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos excluir seu tipo de exame, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
