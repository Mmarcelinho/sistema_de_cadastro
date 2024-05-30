using System;

namespace SistemaDeCadastro.Exceptions.ExceptionsBase;

    public class SistemaDeCadastroException : SystemException
    {
        public SistemaDeCadastroException(string mensagem) : base(mensagem) { }
    }
