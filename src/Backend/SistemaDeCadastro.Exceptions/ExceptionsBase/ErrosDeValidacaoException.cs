using System.Collections.Generic;

namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

    public class ErrosDeValidacaoException : SistemaDeCadastroException
    {
        public List<string> MensagemDeErro { get; set; }

        public ErrosDeValidacaoException(List<string> mensagemDeErro) : base(string.Empty) => MensagemDeErro = mensagemDeErro;
    }
