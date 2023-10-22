using System.Text.Json;
using StocksWebApp.ServiceContracts;

namespace StocksWebApp.Services;

public class FinnhubService : IFinnhubService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<Dictionary<string, object?>> GetStockPriceQuote(string stockSymbol)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        var httpRequestMsg = new HttpRequestMessage()
        {
            RequestUri = new Uri($"https://finnhub.io/api/v1/" +
                                 $"quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
            Method = HttpMethod.Get,
            // Headers = {  }
        };

        var httpResponseMsg = await httpClient.SendAsync(httpRequestMsg);
        var stream = await httpResponseMsg.Content.ReadAsStreamAsync();

        var streamReader = new StreamReader(stream);
        var response = await streamReader.ReadToEndAsync();

        var responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object?>>(response);

        if (responseDictionary is null)
        {
            throw new InvalidOperationException("No response from finnhub");
        }

        if (responseDictionary.TryGetValue("error", out var value))
        {
            throw new InvalidOperationException(
                Convert.ToString(value));
        }
        
        return responseDictionary;
    }
}