namespace SistemaDeCadastro.Domain.ValueObjects;

public record Documento(string Numero, string OrgaoEmissor, string EstadoEmissor, DateTime DataValidade);
