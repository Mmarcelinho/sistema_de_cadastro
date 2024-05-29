namespace SistemaDeCadastro.Domain.Entidades;

    public class Pessoa : EntidadeBase
    {
        public string Cpf { get; set; }

        public string Cnpj { get; set; }

        public string Nome { get; set; }

        public string NomeFantasia { get; set; }

        public string Email { get; set; }

        public DateTime Nascimento { get; set; }

        public int Token { get; set; }

        public List<Domicilio> Domicilios { get; set; }

        public Telefone Telefone { get; set; }

        public long CadastroId { get; set; }
    }
