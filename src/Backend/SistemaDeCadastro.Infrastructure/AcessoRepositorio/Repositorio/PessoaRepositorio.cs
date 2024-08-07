namespace SistemaDeCadastro.Infrastructure.AcessoRepositorio.Repositorio;

public class PessoaRepositorio(SistemaDeCadastroContext contexto) : IPessoaReadOnlyRepositorio, IPessoaWriteOnlyRepositorio, IPessoaUpdateOnlyRepositorio
{
    public async Task<IEnumerable<Pessoa>> RecuperarTodos() => await contexto.Pessoas.AsNoTracking().Include(c => c.Cadastro).ToListAsync();

    public async Task<Pessoa> RecuperarPorId(long pessoaId) => await contexto.Pessoas
    .AsNoTracking()
    .Include(c => c.Cadastro)
    .FirstOrDefaultAsync(pessoa => pessoa.Id == pessoaId);

    async Task<Pessoa> IPessoaUpdateOnlyRepositorio.RecuperarPorId(long pessoaId) => await contexto.Pessoas
    .Include(c => c.Cadastro)
    .FirstOrDefaultAsync(pessoa => pessoa.Id == pessoaId);

    public async Task<bool> RecuperarPessoaExistentePorCnpj(string cnpj) => await contexto.Pessoas.AnyAsync(pessoa => pessoa.Cnpj.Equals(cnpj));

    public async Task<bool> RecuperarPessoaExistentePorCpf(string cpf) => await contexto.Pessoas.AnyAsync(pessoa => pessoa.Cpf.Equals(cpf));

    public async Task Registrar(Pessoa pessoa) => await contexto.Pessoas.AddAsync(pessoa);

    public void Atualizar(Pessoa pessoa) => contexto.Pessoas.Update(pessoa);

    public async Task Deletar(long pessoaId)
    {
        var pessoa = await contexto.Cadastros.FindAsync(pessoaId);

        contexto.Cadastros.Remove(pessoa);
    }
}
