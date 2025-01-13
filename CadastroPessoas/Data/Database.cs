using CadastroPessoas.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CadastroPessoas.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        public void SavePessoa(string nome, string telefone, string cpf)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "INSERT INTO Pessoas (Nome, Telefone, Cpf) VALUES (@Nome, @Telefone, @Cpf)";
                connection.Execute(query, new { Nome = nome, Telefone = telefone, Cpf = cpf });
            }
        }

        public void UpdatePessoa(int id, string nome, string telefone, string cpf)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "UPDATE Pessoas SET Nome = @Nome, Telefone = @Telefone, Cpf = @Cpf WHERE Id = @Id";
                connection.Execute(query, new { Id = id, Nome = nome, Telefone = telefone, Cpf = cpf });
            }
        }

        public IEnumerable<Pessoa> GetPessoas()
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "SELECT * FROM Pessoas";
                return connection.Query<Pessoa>(query).ToList();
            }
        }

        public int GetPessoaIdByCpf(string cpf)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "SELECT Id FROM Pessoas WHERE Cpf = @Cpf";
                return connection.QuerySingleOrDefault<int?>(query, new { Cpf = cpf }) ?? 0;
            }
        }

        public Pessoa GetPessoaById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "SELECT * FROM Pessoas WHERE Id = @Id";
                return connection.QuerySingleOrDefault<Pessoa>(query, new { Id = id }) ?? new Pessoa { Nome = "", Telefone = "", Cpf = "" };
            }
        }

        public void DeletePessoa(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "DELETE FROM Pessoas WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public IEnumerable<Endereco> GetEnderecosByPessoaId(int pessoaId)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "SELECT * FROM Enderecos WHERE PessoaId = @PessoaId";
                return connection.Query<Endereco>(query, new { PessoaId = pessoaId }).ToList();
            }
        }

        public void SaveEndereco(int pessoaId, string enderecoDescricao, string cep, string cidade, string estado)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "INSERT INTO Enderecos (PessoaId, EnderecoDescricao, Cep, Cidade, Estado) VALUES (@PessoaId, @EnderecoDescricao, @Cep, @Cidade, @Estado)";
                connection.Execute(query, new { PessoaId = pessoaId, EnderecoDescricao = enderecoDescricao, Cep = cep, Cidade = cidade, Estado = estado });
            }
        }

        public void DeleteEndereco(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var endereco = connection.QueryFirstOrDefault<Endereco>("SELECT * FROM Enderecos WHERE Id = @Id", new { Id = id });

                if (endereco != null)
                {
                    var query = "DELETE FROM Enderecos WHERE Id = @Id";
                    connection.Execute(query, new { Id = id });
                }
            }
        }

        public Endereco GetEnderecoById(int id)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "SELECT * FROM Enderecos WHERE Id = @Id";
                var result = connection.QuerySingleOrDefault<Endereco>(query, new { Id = id });
                return result ?? new Endereco { EnderecoDescricao = "", Cep = "", Cidade = "", Estado = "" };
            }
        }

        public void UpdateEndereco(int id, string enderecoDescricao, string cep, string cidade, string estado)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "UPDATE Enderecos SET EnderecoDescricao = @EnderecoDescricao, Cep = @Cep, Cidade = @Cidade, Estado = @Estado WHERE Id = @Id";
                connection.Execute(query, new { Id = id, EnderecoDescricao = enderecoDescricao, Cep = cep, Cidade = cidade, Estado = estado });
            }
        }

        public int GetPessoaIdByEnderecoId(int enderecoId)
        {
            using (var connection = Connection)
            {
                connection.Open();
                var query = "SELECT PessoaId FROM Enderecos WHERE Id = @Id";
                return connection.QuerySingleOrDefault<int?>(query, new { Id = enderecoId }) ?? 0;
            }
        }
    }
}
