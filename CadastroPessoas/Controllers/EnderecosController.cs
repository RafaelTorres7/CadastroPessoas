using Microsoft.AspNetCore.Mvc;
using CadastroPessoas.Data;
using CadastroPessoas.Models;

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
            var enderecos = _database.GetEnderecosByPessoaId(pessoaId);
            ViewBag.PessoaId = pessoaId;
            return View(enderecos);
        }

        public IActionResult Create(int pessoaId)
        {
            ViewBag.PessoaId = pessoaId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(int pessoaId, string enderecoDescricao, string cep, string cidade, string estado)
        {
            _database.SaveEndereco(pessoaId, enderecoDescricao, cep, cidade, estado);
            return RedirectToAction("Index", new { pessoaId });
        }

        public IActionResult Delete(int id)
        {
            _database.DeleteEndereco(id);
            return RedirectToAction("Index");
        }
    }
}
