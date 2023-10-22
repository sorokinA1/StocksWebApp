namespace StocksWebApp.Services;

public class NewService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public NewService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task CreateClient()
    {
        using var httpClient = _httpClientFactory.CreateClient();
        var httpRequestMsg = new HttpRequestMessage()
        {
            RequestUri = new Uri("url"),
            Method = HttpMethod.Get,
            // Headers = {  }
        };

        var httpResponseMsg = await httpClient.SendAsync(httpRequestMsg);
    }
}