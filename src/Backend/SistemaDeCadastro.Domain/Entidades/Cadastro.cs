namespace SistemaDeCadastro.Domain.Entidades;

    public class Cadastro : EntidadeBase
    {
        public string Email { get; set; }

        public string NomeFantasia { get; set; }

        public string SobrenomeSocial { get; set; }

        public bool Empresa { get; set; }

        public Credencial Credencial { get; set; }

        public Inscrito Inscrito { get; set; }

        public Parceiro Parceiro { get; set; }

        public Documento Documento { get; set; }

        public Identificacao Identificador { get; set; }
    }
