using Microsoft.AspNetCore.Mvc;
using CadastroPessoas.Data;
using CadastroPessoas.Models;
using Dapper;

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
        public IActionResult Create(Pessoa pessoa)
        {
            if (string.IsNullOrEmpty(pessoa.Nome) || string.IsNullOrEmpty(pessoa.Telefone) || string.IsNullOrEmpty(pessoa.Cpf))
            {
                ModelState.AddModelError("", "Todos os campos são obrigatórios.");
                return View(pessoa);
            }

            // Verificar se o CPF já existe no banco de dados
            var pessoaExistente = _database.GetPessoaIdByCpf(pessoa.Cpf);

            if (pessoaExistente > 0)
            {
                // Adicionar um erro se o CPF já existir
                ModelState.AddModelError("Cpf", "O CPF informado já está cadastrado.");
                return View(pessoa);
            }

            _database.SavePessoa(pessoa.Nome, pessoa.Telefone, pessoa.Cpf);

            var pessoaId = _database.GetPessoaIdByCpf(pessoa.Cpf);

            if (pessoaId > 0)
            {
                var pessoaCriada = _database.GetPessoaById(pessoaId);
                return RedirectToAction("Edit", new { id = pessoaCriada.Id });
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var pessoa = _database.GetPessoaById(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            var enderecos = _database.GetEnderecosByPessoaId(id).ToList();
            pessoa.Enderecos = enderecos;

            return View(pessoa);
        }

        [HttpPost]
        public IActionResult Edit(int id, string nome, string telefone, string cpf)
        {
            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(cpf))
            {
                ModelState.AddModelError("", "Todos os campos são obrigatórios.");
                return View();
            }

            _database.UpdatePessoa(id, nome, telefone, cpf);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var enderecos = _database.GetEnderecosByPessoaId(id);

                foreach (var endereco in enderecos)
                {
                    _database.DeleteEndereco(endereco.Id);
                }

                _database.DeletePessoa(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Erro ao excluir: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [HttpPost]
        public IActionResult AddEndereco(int pessoaId, string cep, string enderecoDescricao, string cidade, string estado)
        {
            if (string.IsNullOrEmpty(cep) || string.IsNullOrEmpty(enderecoDescricao) || string.IsNullOrEmpty(cidade) || string.IsNullOrEmpty(estado))
            {
                ModelState.AddModelError("", "Todos os campos de endereço são obrigatórios.");
                return RedirectToAction("Edit", new { id = pessoaId });
            }

            // Aqui você salvaria os dados do endereço no banco de dados
            _database.SaveEndereco(pessoaId, enderecoDescricao, cep, cidade, estado);
            return RedirectToAction("Edit", new { id = pessoaId });
        }


        [HttpPost]
        public IActionResult EditEndereco(int id, string enderecoDescricao, string cep, string cidade, string estado)
        {
            if (string.IsNullOrEmpty(enderecoDescricao) || string.IsNullOrEmpty(cep) || string.IsNullOrEmpty(cidade) || string.IsNullOrEmpty(estado))
            {
                ModelState.AddModelError("", "Todos os campos são obrigatórios.");
                return View();
            }

            _database.UpdateEndereco(id, enderecoDescricao, cep, cidade, estado);

            var pessoaId = _database.GetPessoaIdByEnderecoId(id);
            return RedirectToAction("Edit", "Pessoas", new { id = pessoaId });
        }
    }
}
