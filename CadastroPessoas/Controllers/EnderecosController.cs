using Microsoft.AspNetCore.Mvc;
using CadastroPessoas.Data;
using CadastroPessoas.Models;
using Dapper;

namespace CadastroPessoas.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly Database _database;

        public EnderecosController(Database database)
        {
            _database = database;
        }

        public IActionResult Index(int pessoaId)
        {
            var enderecos = _database.GetEnderecosByPessoaId(pessoaId).ToList();
            ViewBag.PessoaId = pessoaId;
            return View(enderecos);
        }

        [HttpPost]
        public IActionResult AddEndereco(int pessoaId, string enderecoDescricao, string cep, string cidade, string estado)
        {
            if (string.IsNullOrEmpty(enderecoDescricao) || string.IsNullOrEmpty(cep) || string.IsNullOrEmpty(cidade) || string.IsNullOrEmpty(estado))
            {
                ModelState.AddModelError("", "Todos os campos de endereço são obrigatórios.");
                var pessoa = _database.GetPessoaById(pessoaId);
                var enderecos = _database.GetEnderecosByPessoaId(pessoaId).ToList();
                pessoa.Enderecos = enderecos;
                return View("Edit", pessoa);
            }

            _database.SaveEndereco(pessoaId, enderecoDescricao, cep, cidade, estado);

            return RedirectToAction("Edit", "Pessoas", new { id = pessoaId });
        }

        public IActionResult Edit(int id)
        {
            var endereco = _database.GetEnderecoById(id);
            if (endereco == null)
            {
                return NotFound();
            }
            return View(endereco);
        }

        [HttpPost]
        public IActionResult Edit(int id, string enderecoDescricao, string cep, string cidade, string estado)
        {
            if (string.IsNullOrEmpty(enderecoDescricao) || string.IsNullOrEmpty(cep) || string.IsNullOrEmpty(cidade) || string.IsNullOrEmpty(estado))
            {
                ModelState.AddModelError("", "Todos os campos são obrigatórios.");
                var endereco = _database.GetEnderecoById(id);
                return View(endereco);
            }

            _database.UpdateEndereco(id, enderecoDescricao, cep, cidade, estado);

            var pessoaId = _database.GetPessoaIdByEnderecoId(id);

            return RedirectToAction("Edit", "Pessoas", new { id = pessoaId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var endereco = _database.GetEnderecoById(id);

                if (endereco == null)
                {
                    return NotFound();
                }

                var pessoaId = endereco.PessoaId;

                _database.DeleteEndereco(id);

                return RedirectToAction("Edit", "Pessoas", new { id = pessoaId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao excluir: {ex.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
