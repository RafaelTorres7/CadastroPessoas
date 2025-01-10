namespace CadastroPessoas.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public required string EnderecoDescricao { get; set; }
        public required string Cep { get; set; }
        public required string Cidade { get; set; }
        public required string Estado { get; set; }
    }
}
