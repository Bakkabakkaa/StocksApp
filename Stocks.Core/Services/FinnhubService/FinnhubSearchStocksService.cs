using System.Text.Json;
using Exceptions;
using ServiceContracts;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;


namespace Services;

public class FinnhubSearchStocksService : IFinnhubSearchStocksService
{

    private readonly IFinnhubRepository _finnhubRepository;

    public FinnhubSearchStocksService(IFinnhubRepository finnhubRepository)
    {
        _finnhubRepository = finnhubRepository;
    }
    
    public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
    {
        try
        {
            // Invoke repository
            Dictionary<string, object>? responseDictionary = await _finnhubRepository.SearchStocks(stockSymbolToSearch);

            // Return response dictionary back to the caller
            return responseDictionary;
        }
        catch (Exception ex)
        {
            FinnhubException finnhubException = new FinnhubException("Unable to connect to finnhub", ex);
            throw finnhubException;
        }
    }
}