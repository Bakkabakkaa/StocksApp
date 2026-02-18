using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;

namespace Repositories;

public class FinnhubRepository : IFinnhubRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }
    public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
    {
        Uri url = new Uri(
            $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");
        
        return await SendRequestAsync<Dictionary<string, object>>(url);
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        Uri url = new Uri(
            $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}");

        return await SendRequestAsync<Dictionary<string, object>>(url);
    }

    public async Task<List<Dictionary<string, string>>?> GetStocks()
    {
        Uri url = new Uri(
            $"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["FinnhubToken"]}");
        
        return await SendRequestAsync<List<Dictionary<string, string>>>(url);
    }

    public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
    {
        Uri url = new Uri(
            $"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["FinnhubToken"]}");


        return await SendRequestAsync<Dictionary<string, object>>(url);
    }

    private async Task<T?> SendRequestAsync<T>(Uri url)
    {
        // Create http client
        HttpClient httpClient = _httpClientFactory.CreateClient();
        
        // Create http request
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = url
        };
        
        // Send request
        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        
        // Read response body
        string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
        
        // Convert response body (from JSON into Dictionary)
        T? responseDictionary =
            JsonSerializer.Deserialize<T>(responseBody);

        if (responseDictionary == null)
            throw new InvalidOperationException("No response fron server");

        if (responseDictionary is Dictionary<string, object> dict &&
            dict.ContainsKey("error"))
        {
            throw new InvalidOperationException(
                Convert.ToString(dict["error"])
            );
        }
        
        // Return response dictionary back to the caller
        return responseDictionary;
    }
}