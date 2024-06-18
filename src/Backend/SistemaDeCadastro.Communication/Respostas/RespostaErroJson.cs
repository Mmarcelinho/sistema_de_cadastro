namespace SistemaDeCadastro.Communication.Respostas;

public class RespostaErroJson
{
    public List<string> Mensagens { get; set; }

    public RespostaErroJson(string mensagem)
    {
        Mensagens = [mensagem];
    }

    public RespostaErroJson(List<string> mensagens) => Mensagens = mensagens;
    
}
