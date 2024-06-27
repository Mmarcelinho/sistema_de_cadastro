namespace SistemaDeCadastro.Infrastructure.Servicos.Caching;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1200)
        };
    }

    public async Task Registrar(string chave, string valor)
    => await _cache.SetStringAsync(chave, valor, _options);

    public async Task<string> Recuperar(string chave)
    => await _cache.GetStringAsync(chave);
}
