namespace WebApi.Test;

public class SistemaDeCadastroClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public SistemaDeCadastroClassFixture(CustomWebApplicationFactory webApplicationFactory)
    =>
        _httpClient = webApplicationFactory.CreateClient();

    protected async Task<HttpResponseMessage> DoGet(string requestUri)
    =>  await _httpClient.GetAsync(requestUri);
    

    protected async Task<HttpResponseMessage> DoPost(string requestUri, object request)
    => await _httpClient.PostAsJsonAsync(requestUri, request);
    
}
