namespace SistemaDeCadastro.Domain.Entidades;

    public class Domicilio : EntidadeBase
    {
        public DomicilioTipo Tipo { get; set; }

        public Endereco Endereco { get; set; }
    }
