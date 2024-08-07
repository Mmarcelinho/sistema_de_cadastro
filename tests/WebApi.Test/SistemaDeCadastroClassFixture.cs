namespace WebApi.Test;

public class SistemaDeCadastroClassFixture(CustomWebApplicationFactory webApplicationFactory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient = webApplicationFactory.CreateClient();

    protected async Task<HttpResponseMessage> DoGet(string requestUri)
    =>  await _httpClient.GetAsync(requestUri);
    
    protected async Task<HttpResponseMessage> DoPost(string requestUri, object request)
    => await _httpClient.PostAsJsonAsync(requestUri, request);

    protected async Task<HttpResponseMessage> DoPut(string requestUri, object request)
    => await _httpClient.PutAsJsonAsync(requestUri, request);

    protected async Task<HttpResponseMessage> DoDelete(string requestUri)
    => await _httpClient.DeleteAsync(requestUri);
    
}
