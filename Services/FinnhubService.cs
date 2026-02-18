using System.Text.Json;
using ServiceContracts;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;


namespace Services;

public class FinnhubService : IFinnhubService
{

    private readonly IFinnhubRepository _finnhubRepository;

    public FinnhubService(IFinnhubRepository finnhubRepository)
    {
        _finnhubRepository = finnhubRepository;
    }
    
    public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
    {
        // Invoke repository
        Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetCompanyProfile(stockSymbol);
        
        // Return response dictionary back to the caller
        return responseDictionary;
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
    {
        // Invoke repository
        Dictionary<string, object>? responseDictionary = await _finnhubRepository.GetStockPriceQuote(stockSymbol);
        
        // Return response dictionary back to the caller
        return responseDictionary;
    }

    public async Task<List<Dictionary<string, string>>?> GetStocks()
    {
        
        // Invoke repository
        List<Dictionary<string, string>>? responseDictionary = await _finnhubRepository.GetStocks();
        
        // Return response dictionary back to the caller
        return responseDictionary;
    }

    public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
    {
        // Invoke repository
        Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);
        
        // Return response dictionary back to the caller
        return responseDictionary;
    }
}