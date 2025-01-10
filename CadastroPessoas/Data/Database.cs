using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using CadastroPessoas.Models;

namespace CadastroPessoas.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string is missing.");
        }

        private SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not set.");
            }
            return new SqlConnection(_connectionString);
        }

        public List<Pessoa> GetPessoas()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Pessoas";
                return connection.Query<Pessoa>(query).ToList() ?? new List<Pessoa>(); // Verificando e retornando uma lista vazia caso seja nulo
            }
        }

        public Pessoa GetPessoaById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Pessoas WHERE Id = @Id";
                var pessoa = connection.QueryFirstOrDefault<Pessoa>(query, new { Id = id });

                // Garantir que a pessoa tenha os campos obrigatórios preenchidos
                return pessoa ?? new Pessoa { Nome = "Desconhecido", Telefone = "Desconhecido", Cpf = "00000000000" }; // Inicializar com valores default
            }
        }

        public void SavePessoa(string nome, string telefone, string cpf)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "INSERT INTO Pessoas (Nome, Telefone, Cpf) VALUES (@Nome, @Telefone, @Cpf)";
                connection.Execute(query, new { Nome = nome, Telefone = telefone, Cpf = cpf });
            }
        }

        public void UpdatePessoa(int id, string nome, string telefone, string cpf)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "UPDATE Pessoas SET Nome = @Nome, Telefone = @Telefone, Cpf = @Cpf WHERE Id = @Id";
                connection.Execute(query, new { Id = id, Nome = nome, Telefone = telefone, Cpf = cpf });
            }
        }

        public void DeletePessoa(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "DELETE FROM Pessoas WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public List<Endereco> GetEnderecosByPessoaId(int pessoaId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Enderecos WHERE PessoaId = @PessoaId";
                return connection.Query<Endereco>(query, new { PessoaId = pessoaId }).ToList() ?? new List<Endereco>(); // Verificando e retornando uma lista vazia caso seja nulo
            }
        }

        public void SaveEndereco(int pessoaId, string enderecoDescricao, string cep, string cidade, string estado)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "INSERT INTO Enderecos (PessoaId, EnderecoDescricao, Cep, Cidade, Estado) " +
                            "VALUES (@PessoaId, @EnderecoDescricao, @Cep, @Cidade, @Estado)";
                connection.Execute(query, new { PessoaId = pessoaId, EnderecoDescricao = enderecoDescricao, Cep = cep, Cidade = cidade, Estado = estado });
            }
        }

        public void DeleteEndereco(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var query = "DELETE FROM Enderecos WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }
    }
}
