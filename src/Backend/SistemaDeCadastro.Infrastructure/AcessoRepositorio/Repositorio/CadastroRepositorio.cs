namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;

public class CadastroRepositorio(SistemaDeCadastroContext contexto) : ICadastroReadOnlyRepositorio, ICadastroWriteOnlyRepositorio, ICadastroUpdateOnlyRepositorio
{
    public async Task<IEnumerable<Cadastro>> RecuperarTodos() => await contexto.Cadastros.AsNoTracking().ToListAsync();

    async Task<Cadastro> ICadastroReadOnlyRepositorio.RecuperarPorId(long cadastroId) => await contexto.Cadastros
    .AsNoTracking()
    .FirstOrDefaultAsync(cadastro => cadastro.Id == cadastroId);

    async Task<Cadastro> ICadastroUpdateOnlyRepositorio.RecuperarPorId(long cadastroId) => await contexto.Cadastros.FirstOrDefaultAsync(cadastro => cadastro.Id == cadastroId);

    public async Task<bool> RecuperarCadastroExistentePorEmail(string email) => await contexto.Cadastros.AnyAsync(cadastro => cadastro.Email.Equals(email));

    public async Task Registrar(Cadastro cadastro) => await contexto.Cadastros.AddAsync(cadastro);

    public void Atualizar(Cadastro cadastro) => contexto.Cadastros.Update(cadastro);

    public async Task Deletar(long cadastroId)
    {
        var cadastro = await contexto.Cadastros.FindAsync(cadastroId);

        contexto.Cadastros.Remove(cadastro);
    }
}