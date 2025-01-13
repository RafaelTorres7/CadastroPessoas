namespace CadastroPessoas.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Telefone { get; set; }
        public required string Cpf { get; set; }

        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}
