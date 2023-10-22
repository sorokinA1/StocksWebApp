using System.Text.Json;

namespace StocksWebApp.Services;

public class FinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FinnhubService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task CreateClient()
    {
        using var httpClient = _httpClientFactory.CreateClient();
        var httpRequestMsg = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://finnhub.io/api/v1/" +
                                 "quote?symbol=AAPL&token=ckqbkr9r" +
                                 "01qhi026fes0ckqbkr9r01qhi026fesg"),
            Method = HttpMethod.Get,
            // Headers = {  }
        };

        var httpResponseMsg = await httpClient.SendAsync(httpRequestMsg);
        var stream = await httpResponseMsg.Content.ReadAsStreamAsync();

        var streamReader = new StreamReader(stream);
        var response = await streamReader.ReadToEndAsync();

        var responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object?>>(response);
    }
}