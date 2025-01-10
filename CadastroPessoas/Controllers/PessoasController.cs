using Microsoft.AspNetCore.Mvc;
using CadastroPessoas.Data;
using CadastroPessoas.Models;

namespace CadastroPessoas.Controllers
{
    public class PessoasController : Controller
    {
        private readonly Database _database;

        public PessoasController(Database database)
        {
            _database = database;
        }

        public IActionResult Index()
        {
            var pessoas = _database.GetPessoas();
            return View(pessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string nome, string telefone, string cpf)
        {
            _database.SavePessoa(nome, telefone, cpf);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var pessoa = _database.GetPessoaById(id);
            return View(pessoa);
        }

        [HttpPost]
        public IActionResult Edit(int id, string nome, string telefone, string cpf)
        {
            _database.UpdatePessoa(id, nome, telefone, cpf);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _database.DeletePessoa(id);
            return RedirectToAction("Index");
        }

    }
}
