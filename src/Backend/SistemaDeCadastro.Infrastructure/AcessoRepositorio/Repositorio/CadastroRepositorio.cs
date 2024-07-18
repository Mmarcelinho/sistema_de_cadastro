namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;

public class CadastroRepositorio : ICadastroReadOnlyRepositorio, ICadastroWriteOnlyRepositorio, ICadastroUpdateOnlyRepositorio
{
    private readonly SistemaDeCadastroContext _contexto;

    public CadastroRepositorio(SistemaDeCadastroContext contexto) => _contexto = contexto;
    public async Task<IEnumerable<Cadastro>> RecuperarTodos() => await _contexto.Cadastros.AsNoTracking().ToListAsync();

    async Task<Cadastro> ICadastroReadOnlyRepositorio.RecuperarPorId(long cadastroId) => await _contexto.Cadastros
    .AsNoTracking()
    .FirstOrDefaultAsync(cadastro => cadastro.Id == cadastroId);

    async Task<Cadastro> ICadastroUpdateOnlyRepositorio.RecuperarPorId(long cadastroId) => await _contexto.Cadastros.FirstOrDefaultAsync(cadastro => cadastro.Id == cadastroId);

    public async Task<bool> RecuperarCadastroExistentePorEmail(string email) => await _contexto.Cadastros.AnyAsync(cadastro => cadastro.Email.Equals(email));

    public async Task Registrar(Cadastro cadastro) => await _contexto.Cadastros.AddAsync(cadastro);

    public void Atualizar(Cadastro cadastro) => _contexto.Cadastros.Update(cadastro);

    public async Task Deletar(long cadastroId)
    {
        var cadastro = await _contexto.Cadastros.FindAsync(cadastroId);

        _contexto.Cadastros.Remove(cadastro);
    }
}