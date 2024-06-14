namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;

public class PessoaRepositorio : IPessoaReadOnlyRepositorio, IPessoaWriteOnlyRepositorio, IPessoaUpdateOnlyRepositorio
{
    private readonly SistemaDeCadastroContext _contexto;

    public PessoaRepositorio(SistemaDeCadastroContext contexto) => _contexto = contexto;

    public async Task<IEnumerable<Pessoa>> RecuperarTodos() => await _contexto.Pessoas.AsNoTracking().ToListAsync();

    public async Task<Pessoa> RecuperarPorId(long pessoaId) => await _contexto.Pessoas
    .AsNoTracking()
    .Include(c => c.Cadastro)
    .FirstOrDefaultAsync(pessoa => pessoa.Id == pessoaId);

    async Task<Pessoa> IPessoaUpdateOnlyRepositorio.RecuperarPorId(long pessoaId) => await _contexto.Pessoas
    .Include(c => c.Cadastro)
    .FirstOrDefaultAsync(pessoa => pessoa.Id == pessoaId);

    public async Task<bool> RecuperarPessoaExistentePorCnpj(string cnpj) => await _contexto.Pessoas.AnyAsync(pessoa => pessoa.Cnpj.Equals(cnpj));

    public async Task<bool> RecuperarPessoaExistentePorCpf(string cpf) => await _contexto.Pessoas.AnyAsync(pessoa => pessoa.Cpf.Equals(cpf));

    public async Task Registrar(Pessoa pessoa) => await _contexto.Pessoas.AddAsync(pessoa);

    public void Atualizar(Pessoa pessoa) => _contexto.Pessoas.Update(pessoa);

    public async Task<bool> Deletar(long pessoaId)
    {
        var resultado = await _contexto.Pessoas.FirstOrDefaultAsync(pessoa => pessoa.Id == pessoaId);

        if (resultado is null)
            return false;

        _contexto.Pessoas.Remove(resultado);

        return true;
    }
}
